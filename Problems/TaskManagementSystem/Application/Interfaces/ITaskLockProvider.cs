namespace TaskManagementSystem.Application.Interfaces;

public interface ITaskLockProvider
{
    IDisposable Acquire(Guid taskId);
}
