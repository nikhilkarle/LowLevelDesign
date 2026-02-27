using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Builders;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.Events;

namespace TaskManagementSystem.Application.Services;

public sealed class TaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITaskHistoryRepository _historyRepository;
    private readonly IReminderScheduler _reminderScheduler;
    private readonly ITaskLockProvider _lockProvider;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IEnumerable<ITaskEventListener<TaskAssignedEvent>> _assignedListeners;
    private readonly IEnumerable<ITaskEventListener<TaskCompletedEvent>> _completedListeners;

    public TaskService(
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        ITaskHistoryRepository historyRepository,
        IReminderScheduler reminderScheduler,
        ITaskLockProvider lockProvider,
        IUnitOfWork unitOfWork,
        IEnumerable<ITaskEventListener<TaskAssignedEvent>> assignedListeners,
        IEnumerable<ITaskEventListener<TaskCompletedEvent>> completedListeners)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _historyRepository = historyRepository;
        _reminderScheduler = reminderScheduler;
        _lockProvider = lockProvider;
        _unitOfWork = unitOfWork;
        _assignedListeners = assignedListeners;
        _completedListeners = completedListeners;
    }

    public Guid CreateTask(CreateTaskRequest request)
    {
        var task = new TaskBuilder()
            .WithTitle(request.Title)
            .WithDescription(request.Description)
            .WithDueDateUtc(request.DueDateUtc)
            .WithPriority(request.Priority)
            .Build();

        if (request.AssignedUserId.HasValue)
        {
            EnsureUserExists(request.AssignedUserId.Value);
            task.AssignTo(request.AssignedUserId.Value);
        }

        foreach (int minutes in request.ReminderOffsetsInMinutes.Distinct())
        {
            DateTime triggerAt = task.DueDateUtc.AddMinutes(-minutes);
            if (triggerAt <= DateTime.UtcNow) continue;

            var reminder = new Reminder(Guid.NewGuid(), triggerAt, $"{minutes} minutes left for '{task.Title}'");
            task.AddReminder(reminder);
        }

        _taskRepository.Add(task);

        _historyRepository.Add(new TaskHistoryEntry(
            Guid.NewGuid(),
            task.Id,
            TaskEventType.Created,
            DateTime.UtcNow,
            $"Task created: {task.Title}"
        ));

        foreach (var reminder in task.Reminders)
        {
            _reminderScheduler.Schedule(task.Id, reminder);
            _historyRepository.Add(new TaskHistoryEntry(
                Guid.NewGuid(),
                task.Id,
                TaskEventType.ReminderScheduled,
                DateTime.UtcNow,
                $"Reminder scheduled at {reminder.TriggerAtUtc:u}"
            ));
        }

        if (task.AssignedUserId.HasValue)
        {
            var evt = new TaskAssignedEvent(task.Id, task.AssignedUserId.Value, DateTime.UtcNow);
            foreach (var listener in _assignedListeners)
                listener.Handle(evt);
        }

        _unitOfWork.Commit();
        return task.Id;
    }

    public void UpdateTask(Guid taskId, UpdateTaskRequest request)
    {
        using var taskLock = _lockProvider.Acquire(taskId);

        TaskItem task = _taskRepository.GetById(taskId) ?? throw new KeyNotFoundException("Task not found.");
        task.UpdateDetails(request.Title, request.Description, request.DueDateUtc, request.Priority);

        _taskRepository.Update(task, request.ExpectedVersion);

        _historyRepository.Add(new TaskHistoryEntry(
            Guid.NewGuid(),
            taskId,
            TaskEventType.Updated,
            DateTime.UtcNow,
            "Task details updated"
        ));

        _unitOfWork.Commit();
    }

    public void AssignTask(Guid taskId, Guid userId, long expectedVersion)
    {
        EnsureUserExists(userId);

        using var taskLock = _lockProvider.Acquire(taskId);

        TaskItem task = _taskRepository.GetById(taskId) ?? throw new KeyNotFoundException("Task not found.");
        task.AssignTo(userId);

        _taskRepository.Update(task, expectedVersion);

        var evt = new TaskAssignedEvent(taskId, userId, DateTime.UtcNow);
        foreach (var listener in _assignedListeners)
            listener.Handle(evt);

        _unitOfWork.Commit();
    }

    public void StartTask(Guid taskId, long expectedVersion)
    {
        using var taskLock = _lockProvider.Acquire(taskId);

        TaskItem task = _taskRepository.GetById(taskId) ?? throw new KeyNotFoundException("Task not found.");
        task.Start();
        _taskRepository.Update(task, expectedVersion);

        _historyRepository.Add(new TaskHistoryEntry(
            Guid.NewGuid(),
            taskId,
            TaskEventType.Updated,
            DateTime.UtcNow,
            "Task moved to InProgress"
        ));

        _unitOfWork.Commit();
    }

    public void CompleteTask(Guid taskId, long expectedVersion)
    {
        using var taskLock = _lockProvider.Acquire(taskId);

        TaskItem task = _taskRepository.GetById(taskId) ?? throw new KeyNotFoundException("Task not found.");
        task.Complete();
        _taskRepository.Update(task, expectedVersion);

        var evt = new TaskCompletedEvent(taskId, DateTime.UtcNow);
        foreach (var listener in _completedListeners)
            listener.Handle(evt);

        _unitOfWork.Commit();
    }

    public void DeleteTask(Guid taskId, long expectedVersion)
    {
        using var taskLock = _lockProvider.Acquire(taskId);

        _taskRepository.Delete(taskId, expectedVersion);

        _historyRepository.Add(new TaskHistoryEntry(
            Guid.NewGuid(),
            taskId,
            TaskEventType.Deleted,
            DateTime.UtcNow,
            "Task deleted"
        ));

        _unitOfWork.Commit();
    }

    public IReadOnlyList<TaskHistoryEntry> GetTaskHistory(Guid taskId)
    {
        return _historyRepository.GetByTaskId(taskId);
    }

    private void EnsureUserExists(Guid userId)
    {
        if (_userRepository.GetById(userId) is null)
            throw new InvalidOperationException("Assigned user does not exist.");
    }
}