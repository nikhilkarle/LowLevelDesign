using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Specifications;

public sealed class AssignedUserSpecification : ITaskSpecification
{
    private readonly Guid _userId;

    public AssignedUserSpecification(Guid userId) => _userId = userId;

    public bool IsSatisfiedBy(TaskItem task) => task.AssignedUserId == _userId;
}
