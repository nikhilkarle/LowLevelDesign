using Facebook.Application.Interfaces;
using Facebook.Application.Observers;
using Facebook.Domain.Entities;
using Facebook.Domain.Enums;

namespace Facebook.Application.Services;

public sealed class NotificationService : INotificationSubject
{
    private static NotificationService? _instance;
    private static readonly object _lock = new();

    private readonly INotificationRepository _notificationRepository;
    private readonly Dictionary<Guid, List<INotificationObserver>> _subscribers = new();

    private NotificationService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public static NotificationService GetInstance(INotificationRepository notificationRepository)
    {
        if (_instance is null)
        {
            lock (_lock)
            {
                _instance ??= new NotificationService(notificationRepository);
            }
        }
        return _instance;
    }

    public void Subscribe(Guid userId, INotificationObserver observer)
    {
        if (!_subscribers.ContainsKey(userId))
            _subscribers[userId] = new List<INotificationObserver>();
        _subscribers[userId].Add(observer);
    }

    public void Unsubscribe(Guid userId, INotificationObserver observer)
    {
        if (_subscribers.TryGetValue(userId, out var observers))
            observers.Remove(observer);
    }

    public void Notify(Notification notification)
    {
        _notificationRepository.Add(notification);

        if (_subscribers.TryGetValue(notification.RecipientId, out var observers))
        {
            foreach (var observer in observers)
                observer.OnNotificationReceived(notification);
        }
    }

    public Notification SendFriendRequestNotification(Guid actorId, Guid recipientId, Guid requestId, string actorName)
    {
        var notification = new Notification(
            Guid.NewGuid(), recipientId, actorId,
            NotificationType.FriendRequest, requestId,
            $"{actorName} sent you a friend request.");
        Notify(notification);
        return notification;
    }

    public Notification SendFriendRequestAcceptedNotification(Guid actorId, Guid recipientId, string actorName)
    {
        var notification = new Notification(
            Guid.NewGuid(), recipientId, actorId,
            NotificationType.FriendRequestAccepted, null,
            $"{actorName} accepted your friend request.");
        Notify(notification);
        return notification;
    }

    public Notification SendLikeNotification(Guid actorId, Guid postAuthorId, Guid postId, string actorName)
    {
        var notification = new Notification(
            Guid.NewGuid(), postAuthorId, actorId,
            NotificationType.Like, postId,
            $"{actorName} liked your post.");
        Notify(notification);
        return notification;
    }

    public Notification SendCommentNotification(Guid actorId, Guid postAuthorId, Guid postId, string actorName)
    {
        var notification = new Notification(
            Guid.NewGuid(), postAuthorId, actorId,
            NotificationType.Comment, postId,
            $"{actorName} commented on your post.");
        Notify(notification);
        return notification;
    }

    public IReadOnlyList<Notification> GetNotificationsForUser(Guid userId)
        => _notificationRepository.GetForUser(userId);

    public IReadOnlyList<Notification> GetUnreadNotifications(Guid userId)
        => _notificationRepository.GetUnreadForUser(userId);
}
