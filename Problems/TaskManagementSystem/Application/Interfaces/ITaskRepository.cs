using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Interfaces;

public interface ITaskRepository
{
    TaskItem? GetById(Guid taskId);
    IReadOnlyList<TaskItem> GetAll();
    void Add(TaskItem task);
    void Update(TaskItem task, long expectedVersion);
    void Delete(Guid taskId, long expectedVersion);
}
