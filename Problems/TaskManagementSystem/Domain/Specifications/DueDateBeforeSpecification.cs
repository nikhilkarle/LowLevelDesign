using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Specifications;

public sealed class DueDateBeforeSpecification : ITaskSpecification
{
    private readonly DateTime _dateUtc;

    public DueDateBeforeSpecification(DateTime dateUtc) => _dateUtc = dateUtc;

    public bool IsSatisfiedBy(TaskItem task) => task.DueDateUtc <= _dateUtc;
}
