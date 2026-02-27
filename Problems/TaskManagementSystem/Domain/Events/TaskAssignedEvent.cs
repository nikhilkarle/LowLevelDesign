namespace TaskManagementSystem.Domain.Events;

public sealed record TaskAssignedEvent(Guid TaskId, Guid AssignedUserId, DateTime OccurredAtUtc);
