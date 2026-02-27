using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Specifications;

public interface ITaskSpecification
{
    bool IsSatisfiedBy(TaskItem task);
}
