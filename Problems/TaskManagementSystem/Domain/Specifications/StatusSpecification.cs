using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Specifications;

public sealed class StatusSpecification : ITaskSpecification
{
    private readonly Enums.TaskStatus _status;

    public StatusSpecification(Enums.TaskStatus status) => _status = status;

    public bool IsSatisfiedBy(TaskItem task) => task.Status == _status;
}
