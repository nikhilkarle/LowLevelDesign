using Facebook.Application.Interfaces;
using Facebook.Domain.Entities;

namespace Facebook.Infrastructure.Repositories;

public class InMemoryNotificationRepository : INotificationRepository
{
    private readonly List<Notification> _store = new();

    public void Add(Notification notification) => _store.Add(notification);

    public IReadOnlyList<Notification> GetForUser(Guid userId)
        => _store.Where(n => n.RecipientId == userId).OrderByDescending(n => n.CreatedAt).ToList();

    public IReadOnlyList<Notification> GetUnreadForUser(Guid userId)
        => _store.Where(n => n.RecipientId == userId && !n.IsRead).OrderByDescending(n => n.CreatedAt).ToList();
}
