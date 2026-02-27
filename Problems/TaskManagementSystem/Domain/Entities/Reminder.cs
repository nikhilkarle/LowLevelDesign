namespace TaskManagementSystem.Domain.Entities;

public sealed class Reminder
{
    public Guid Id { get; }
    public DateTime TriggerAtUtc { get; }
    public string Message { get; }

    public Reminder(Guid id, DateTime triggerAtUtc, string message)
    {
        Id = id;
        TriggerAtUtc = triggerAtUtc;
        Message = message;
    }
}
