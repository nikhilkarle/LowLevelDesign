using System.Collections.Concurrent;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Concurrency;

namespace TaskManagementSystem.Infrastructure.Repositories;

public sealed class InMemoryTaskRepository : ITaskRepository
{
    private readonly ConcurrentDictionary<Guid, TaskItem> _store = new();

    public void Add(TaskItem task)
    {
        if (!_store.TryAdd(task.Id, task))
            throw new InvalidOperationException("Task already exists.");
    }

    public TaskItem? GetById(Guid taskId)
    {
        _store.TryGetValue(taskId, out TaskItem? task);
        return task;
    }

    public IReadOnlyList<TaskItem> GetAll() => _store.Values.ToList();

    public void Update(TaskItem task, long expectedVersion)
    {
        if (!_store.TryGetValue(task.Id, out TaskItem? existing))
            throw new KeyNotFoundException("Task not found.");

        if (existing.Version != expectedVersion)
            throw new ConcurrencyException($"Task {task.Id} was modified by another operation.");

        task.IncrementVersion();
        _store[task.Id] = task;
    }

    public void Delete(Guid taskId, long expectedVersion)
    {
        if (!_store.TryGetValue(taskId, out TaskItem? existing))
            throw new KeyNotFoundException("Task not found.");

        if (existing.Version != expectedVersion)
            throw new ConcurrencyException($"Task {taskId} was modified by another operation.");

        if (!_store.TryRemove(taskId, out _))
            throw new InvalidOperationException("Delete failed.");
    }
}
