namespace TaskManagementSystem.Domain.Events;

public interface ITaskEventListener<in TEvent>
{
    void Handle(TEvent evt);
}
