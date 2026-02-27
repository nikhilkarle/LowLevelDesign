using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs;

public sealed class CreateTaskRequest
{
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public required DateTime DueDateUtc { get; init; }
    public TaskPriority Priority { get; init; } = TaskPriority.Medium;
    public Guid? AssignedUserId { get; init; }
    public List<int> ReminderOffsetsInMinutes { get; init; } = new();
}
