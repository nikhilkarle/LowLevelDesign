using System.Collections.Concurrent;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Repositories;

public sealed class InMemoryTaskHistoryRepository : ITaskHistoryRepository
{
    private readonly ConcurrentBag<TaskHistoryEntry> _entries = new();

    public void Add(TaskHistoryEntry entry) => _entries.Add(entry);

    public IReadOnlyList<TaskHistoryEntry> GetByTaskId(Guid taskId) =>
        _entries.Where(e => e.TaskId == taskId)
               .OrderBy(e => e.OccurredAtUtc)
               .ToList();

    public IReadOnlyList<TaskHistoryEntry> GetByUserTasks(IEnumerable<Guid> taskIds)
    {
        HashSet<Guid> taskIdSet = taskIds.ToHashSet();
        return _entries.Where(e => taskIdSet.Contains(e.TaskId))
                       .OrderBy(e => e.OccurredAtUtc)
                       .ToList();
    }
}
