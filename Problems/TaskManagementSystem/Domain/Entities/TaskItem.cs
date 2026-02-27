using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.State;

namespace TaskManagementSystem.Domain.Entities;

public sealed class TaskItem
{
    private readonly List<Reminder> _reminders = new();

    public Guid Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDateUtc { get; private set; }
    public TaskPriority Priority { get; private set; }
    public Enums.TaskStatus Status { get; private set; }
    public Guid? AssignedUserId { get; private set; }
    public DateTime CreatedAtUtc { get; }
    public DateTime UpdatedAtUtc { get; private set; }

    public long Version { get; private set; }

    public IReadOnlyList<Reminder> Reminders => _reminders.AsReadOnly();

    internal TaskItem(
        Guid id,
        string title,
        string description,
        DateTime dueDateUtc,
        TaskPriority priority,
        DateTime createdAtUtc)
    {
        Id = id;
        Title = title;
        Description = description;
        DueDateUtc = dueDateUtc;
        Priority = priority;
        Status = Enums.TaskStatus.Pending;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = createdAtUtc;
        Version = 0;
    }

    public void UpdateDetails(string title, string description, DateTime dueDateUtc, TaskPriority priority)
    {
        if (Status == Enums.TaskStatus.Completed)
            throw new InvalidOperationException("Cannot edit a completed task.");

        Title = title;
        Description = description;
        DueDateUtc = dueDateUtc;
        Priority = priority;
        Touch();
    }

    public void AssignTo(Guid userId)
    {
        AssignedUserId = userId;
        Touch();
    }

    public void AddReminder(Reminder reminder)
    {
        _reminders.Add(reminder);
        Touch();
    }

    public void Start()
    {
        ITaskState state = CreateState(Status);
        state.Start();
        Status = Enums.TaskStatus.InProgress;
        Touch();
    }

    public void Complete()
    {
        ITaskState state = CreateState(Status);
        state.Complete();
        Status = Enums.TaskStatus.Completed;
        Touch();
    }

    public void Reopen()
    {
        ITaskState state = CreateState(Status);
        state.Reopen();
        Status = Enums.TaskStatus.Pending;
        Touch();
    }

    public void IncrementVersion()
    {
        Version++;
    }

    private void Touch() => UpdatedAtUtc = DateTime.UtcNow;

    private static ITaskState CreateState(Enums.TaskStatus status) => status switch
    {
        Enums.TaskStatus.Pending => new PendingState(),
        Enums.TaskStatus.InProgress => new InProgressState(),
        Enums.TaskStatus.Completed => new CompletedState(),
        _ => throw new ArgumentOutOfRangeException(nameof(status))
    };
}
