using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs;

public sealed class TaskFilter
{
    public TaskPriority? Priority { get; init; }
    public DateTime? DueBeforeUtc { get; init; }
    public Guid? AssignedUserId { get; init; }
    public Domain.Enums.TaskStatus? Status { get; init; }
}
