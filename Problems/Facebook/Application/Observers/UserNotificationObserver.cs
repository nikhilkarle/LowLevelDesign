using Facebook.Domain.Entities;

namespace Facebook.Application.Observers;

public class UserNotificationObserver : INotificationObserver
{
    private readonly Guid _userId;

    public UserNotificationObserver(Guid userId) => _userId = userId;

    public void OnNotificationReceived(Notification notification)
    {
        Console.WriteLine($"[REALTIME] User {_userId} | {notification.Type}: {notification.Message}");
    }
}
