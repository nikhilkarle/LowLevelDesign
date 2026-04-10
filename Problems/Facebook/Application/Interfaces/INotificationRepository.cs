using Facebook.Domain.Entities;

namespace Facebook.Application.Interfaces;

public interface INotificationRepository
{
    void Add(Notification notification);
    IReadOnlyList<Notification> GetForUser(Guid userId);
    IReadOnlyList<Notification> GetUnreadForUser(Guid userId);
}
