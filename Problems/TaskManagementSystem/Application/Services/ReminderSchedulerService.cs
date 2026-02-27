using System.Collections.Concurrent;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Events;

namespace TaskManagementSystem.Application.Services;

public sealed class ReminderSchedulerService : IReminderScheduler
{
    private readonly ConcurrentQueue<(Guid TaskId, Reminder Reminder)> _queue = new();
    private readonly IEnumerable<ITaskEventListener<ReminderDueEvent>> _reminderListeners;

    public ReminderSchedulerService(IEnumerable<ITaskEventListener<ReminderDueEvent>> reminderListeners)
    {
        _reminderListeners = reminderListeners;
    }

    public void Schedule(Guid taskId, Reminder reminder)
    {
        _queue.Enqueue((taskId, reminder));
        Console.WriteLine($"[Scheduler] Reminder queued for task {taskId} at {reminder.TriggerAtUtc:u}");
    }

    public void ProcessDueReminders(DateTime nowUtc)
    {
        int count = _queue.Count;
        for (int i = 0; i < count; i++)
        {
            if (!_queue.TryDequeue(out var item)) continue;

            if (item.Reminder.TriggerAtUtc <= nowUtc)
            {
                var evt = new ReminderDueEvent(item.TaskId, item.Reminder.Id, item.Reminder.Message, nowUtc);
                foreach (var listener in _reminderListeners)
                    listener.Handle(evt);
            }
            else
            {
                _queue.Enqueue(item);
            }
        }
    }
}