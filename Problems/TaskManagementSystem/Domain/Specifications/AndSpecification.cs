using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Specifications;

public sealed class AndSpecification : ITaskSpecification
{
    private readonly ITaskSpecification _left;
    private readonly ITaskSpecification _right;

    public AndSpecification(ITaskSpecification left, ITaskSpecification right)
    {
        _left = left;
        _right = right;
    }

    public bool IsSatisfiedBy(TaskItem task) =>
        _left.IsSatisfiedBy(task) && _right.IsSatisfiedBy(task);
}
