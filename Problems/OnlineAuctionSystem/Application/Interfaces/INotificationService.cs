using OAS.Domain.Models;

namespace OAS.Application.Interfaces;

public interface INotificationService
{
    void NotifyUser(Guid userId, NotificationMessage message);
    void NotifyUsers(IEnumerable<Guid> userIds, NotificationMessage message);
}