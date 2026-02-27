using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Events;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Infrastructure.Concurrency;
using TaskManagementSystem.Infrastructure.Persistence;
using TaskManagementSystem.Infrastructure.Repositories;

namespace TaskManagementSystem;

public static class Program
{
    public static void Main()
    {
        ITaskRepository taskRepo = new InMemoryTaskRepository();
        IUserRepository userRepo = new InMemoryUserRepository();
        ITaskHistoryRepository historyRepo = new InMemoryTaskHistoryRepository();
        ITaskLockProvider lockProvider = new InMemoryTaskLockProvider();
        IUnitOfWork unitOfWork = new InMemoryUnitOfWork();

        var notificationService = new NotificationService();
        var historyService = new HistoryService(historyRepo);

        var reminderListeners = new List<ITaskEventListener<ReminderDueEvent>> { notificationService };
        var reminderScheduler = new ReminderSchedulerService(reminderListeners);

        var assignedListeners = new List<ITaskEventListener<TaskAssignedEvent>>
        {
            notificationService, historyService
        };

        var completedListeners = new List<ITaskEventListener<TaskCompletedEvent>>
        {
            notificationService, historyService
        };

        var taskService = new TaskService(
            taskRepo,
            userRepo,
            historyRepo,
            reminderScheduler,
            lockProvider,
            unitOfWork,
            assignedListeners,
            completedListeners);

        var queryService = new TaskQueryService(taskRepo);

        Guid userId = Guid.NewGuid();
        userRepo.Add(new User(userId, "Nikhil", "Nikhil@example.com"));

        Guid taskId = taskService.CreateTask(new CreateTaskRequest
        {
            Title = "Prepare design doc",
            Description = "LLD  prep and class diagrams",
            DueDateUtc = DateTime.UtcNow.AddHours(4),
            Priority = TaskPriority.High,
            AssignedUserId = userId,
            ReminderOffsetsInMinutes = new List<int> { 180, 60, 15 }
        });

        var task = taskRepo.GetById(taskId)!;
        long v0 = task.Version;

        taskService.StartTask(taskId, v0);

        var taskAfterStart = taskRepo.GetById(taskId)!;
        taskService.CompleteTask(taskId, taskAfterStart.Version);

        var results = queryService.Search(new TaskFilter
        {
            Priority = TaskPriority.High,
            AssignedUserId = userId
        });

        Console.WriteLine($"Tasks found: {results.Count}");

        var history = taskService.GetTaskHistory(taskId);
        foreach (var h in history)
        {
            Console.WriteLine($"{h.OccurredAtUtc:u} | {h.EventType} | {h.Details}");
        }

        reminderScheduler.ProcessDueReminders(DateTime.UtcNow.AddHours(5));
    }
}