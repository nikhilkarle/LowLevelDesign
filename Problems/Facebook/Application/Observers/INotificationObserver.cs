using Facebook.Domain.Entities;

namespace Facebook.Application.Observers;

public interface INotificationObserver
{
    void OnNotificationReceived(Notification notification);
}
