using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Enums;

namespace TaskManagementSystem.Domain.Builders;

public sealed class TaskBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _title = string.Empty;
    private string _description = string.Empty;
    private DateTime _dueDateUtc = DateTime.UtcNow.AddDays(1);
    private TaskPriority _priority = TaskPriority.Medium;
    private DateTime _createdAtUtc = DateTime.UtcNow;

    public TaskBuilder WithId(Guid id) { _id = id; return this; }
    public TaskBuilder WithTitle(string title) { _title = title; return this; }
    public TaskBuilder WithDescription(string description) { _description = description; return this; }
    public TaskBuilder WithDueDateUtc(DateTime dueDateUtc) { _dueDateUtc = dueDateUtc; return this; }
    public TaskBuilder WithPriority(TaskPriority priority) { _priority = priority; return this; }
    public TaskBuilder WithCreatedAtUtc(DateTime createdAtUtc) { _createdAtUtc = createdAtUtc; return this; }

    public TaskItem Build()
    {
        if (string.IsNullOrWhiteSpace(_title))
            throw new InvalidOperationException("Task title is required.");

        if (_dueDateUtc <= _createdAtUtc)
            throw new InvalidOperationException("Due date must be after creation time.");

        return new TaskItem(_id, _title, _description, _dueDateUtc, _priority, _createdAtUtc);
    }
}
