using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Interfaces;

public interface IReminderScheduler
{
    void Schedule(Guid taskId, Reminder reminder);
}
