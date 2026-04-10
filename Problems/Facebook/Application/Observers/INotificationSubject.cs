using Facebook.Domain.Entities;

namespace Facebook.Application.Observers;

public interface INotificationSubject
{
    void Subscribe(Guid userId, INotificationObserver observer);
    void Unsubscribe(Guid userId, INotificationObserver observer);
    void Notify(Notification notification);
}
