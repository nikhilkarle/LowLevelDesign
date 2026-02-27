using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.State;

public interface ITaskState
{
    Enums.TaskStatus Status { get; }
    void Start();
    void Complete();
    void Reopen();
}
