using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Events;

namespace TaskManagementSystem.Application.Services;

public sealed class NotificationService : INotificationService, ITaskEventListener<TaskAssignedEvent>, ITaskEventListener<TaskCompletedEvent>,
    ITaskEventListener<ReminderDueEvent>
{
    public void NotifyUser(Guid userId, string message)
    {
        Console.WriteLine($"[Notify User {userId}] {message}");
    }

    public void Handle(TaskAssignedEvent evt)
    {
        NotifyUser(evt.AssignedUserId, $"Task {evt.TaskId} has been assigned to you.");
    }

    public void Handle(TaskCompletedEvent evt)
    {
        Console.WriteLine($"[Notification] Task {evt.TaskId} completed at {evt.OccurredAtUtc:u}");
    }

    public void Handle(ReminderDueEvent evt)
    {
        Console.WriteLine($"[Reminder] Task {evt.TaskId}: {evt.Message}");
    }
    
}