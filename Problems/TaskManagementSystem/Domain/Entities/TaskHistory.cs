using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Entities;

public sealed class TaskHistoryEntry
{
    public Guid Id { get; }
    public Guid TaskId { get; }
    public TaskEventType EventType { get; }
    public DateTime OccurredAtUtc { get; }
    public string Details { get; }

    public TaskHistoryEntry(Guid id, Guid taskId, TaskEventType eventType, DateTime occurredAtUtc, string details)
    {
        Id = id;
        TaskId = taskId;
        EventType = eventType;
        OccurredAtUtc = occurredAtUtc;
        Details = details;
    }
}
