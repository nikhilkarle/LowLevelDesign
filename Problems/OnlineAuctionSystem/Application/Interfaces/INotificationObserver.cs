using OAS.Domain.Models;

namespace OAS.Application.Interfaces;

public interface INotificationObserver
{
    Guid UserId { get; }
    void Notify(NotificationMessage message);
}