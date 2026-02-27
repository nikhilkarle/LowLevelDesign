namespace TaskManagementSystem.Domain.Events;

public sealed record ReminderDueEvent(Guid TaskId, Guid ReminderId, string Message, DateTime TriggeredAtUtc);
