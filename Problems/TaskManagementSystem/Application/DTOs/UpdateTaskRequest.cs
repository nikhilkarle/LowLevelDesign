using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Application.DTOs;

public sealed class UpdateTaskRequest
{
    public required string Title { get; init; }
    public string Description { get; init; } = string.Empty;
    public required DateTime DueDateUtc { get; init; }
    public TaskPriority Priority { get; init; }
    public long ExpectedVersion { get; init; } 
}
