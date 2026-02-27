namespace TaskManagementSystem.Domain.Events;

public sealed record TaskCompletedEvent(Guid TaskId, DateTime OccurredAtUtc);
