using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Interfaces;

public interface ITaskHistoryRepository
{
    void Add(TaskHistoryEntry entry);
    IReadOnlyList<TaskHistoryEntry> GetByTaskId(Guid taskId);
    IReadOnlyList<TaskHistoryEntry> GetByUserTasks(IEnumerable<Guid> taskIds);
}
