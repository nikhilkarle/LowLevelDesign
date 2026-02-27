using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.State;

public sealed class CompletedState : ITaskState
{
    public Enums.TaskStatus Status => Enums.TaskStatus.Completed;

    public void Start()
        => throw new InvalidOperationException("Completed task cannot be started.");

    public void Complete()
        => throw new InvalidOperationException("Task is already completed.");

    public void Reopen() {}
}
