using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;
using TaskManagementSystem.Domain.Events;

namespace TaskManagementSystem.Application.Services;

public sealed class HistoryService :
    ITaskEventListener<TaskAssignedEvent>,
    ITaskEventListener<TaskCompletedEvent>
{
    private readonly ITaskHistoryRepository _historyRepository;

    public HistoryService(ITaskHistoryRepository historyRepository)
    {
        _historyRepository = historyRepository;
    }

    public void Handle(TaskAssignedEvent evt)
    {
        _historyRepository.Add(new TaskHistoryEntry(
            Guid.NewGuid(),
            evt.TaskId,
            TaskEventType.Assigned,
            evt.OccurredAtUtc,
            $"Assigned to user {evt.AssignedUserId}"
        ));
    }

    public void Handle(TaskCompletedEvent evt)
    {
        _historyRepository.Add(new TaskHistoryEntry(
            Guid.NewGuid(),
            evt.TaskId,
            TaskEventType.Completed,
            evt.OccurredAtUtc,
            "Task marked as completed"
        ));
    }
}
