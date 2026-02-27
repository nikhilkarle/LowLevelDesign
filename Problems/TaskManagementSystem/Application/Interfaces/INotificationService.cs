namespace TaskManagementSystem.Application.Interfaces;

public interface INotificationService
{
    void NotifyUser(Guid userId, string message);
}
