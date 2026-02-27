using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.State;

public sealed class InProgressState : ITaskState
{
    public Enums.TaskStatus Status => Enums.TaskStatus.InProgress;

    public void Start()
        => throw new InvalidOperationException("Task is already in progress.");

    public void Complete() {}

    public void Reopen() {}
}
