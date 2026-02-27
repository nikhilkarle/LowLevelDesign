using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.State;

public sealed class PendingState : ITaskState
{
    public Enums.TaskStatus Status => Enums.TaskStatus.Pending;

    public void Start() {}

    public void Complete()
        => throw new InvalidOperationException("Pending task must move to InProgress before completion.");

    public void Reopen()
        => throw new InvalidOperationException("Task is already pending.");
}
