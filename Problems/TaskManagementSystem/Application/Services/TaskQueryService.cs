using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Specifications;

namespace TaskManagementSystem.Application.Services;

public sealed class TaskQueryService
{
    private readonly ITaskRepository _taskRepository;

    public TaskQueryService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public IReadOnlyList<TaskItem> Search(TaskFilter filter)
    {
        ITaskSpecification? spec = null;

        if (filter.Priority.HasValue)
            spec = Combine(spec, new PrioritySpecification(filter.Priority.Value));

        if (filter.DueBeforeUtc.HasValue)
            spec = Combine(spec, new DueDateBeforeSpecification(filter.DueBeforeUtc.Value));

        if (filter.AssignedUserId.HasValue)
            spec = Combine(spec, new AssignedUserSpecification(filter.AssignedUserId.Value));

        if (filter.Status.HasValue)
            spec = Combine(spec, new StatusSpecification(filter.Status.Value));

        var tasks = _taskRepository.GetAll();
        return spec is null ? tasks : tasks.Where(t => spec.IsSatisfiedBy(t)).ToList();
    }

    private static ITaskSpecification Combine(ITaskSpecification? left, ITaskSpecification right)
        => left is null ? right : new AndSpecification(left, right);
}