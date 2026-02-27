using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Specifications;

public sealed class PrioritySpecification : ITaskSpecification
{
    private readonly TaskPriority _priority;

    public PrioritySpecification(TaskPriority priority) => _priority = priority;

    public bool IsSatisfiedBy(TaskItem task) => task.Priority == _priority;
}
