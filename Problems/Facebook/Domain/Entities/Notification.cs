using Facebook.Domain.Enums;

namespace Facebook.Domain.Entities;

public class Notification
{
    public Guid Id { get; }
    public Guid RecipientId { get; }
    public Guid ActorId { get; }          
    public NotificationType Type { get; }
    public Guid? ReferenceId { get; }   
    public string Message { get; }
    public bool IsRead { get; private set; }
    public DateTime CreatedAt { get; }

    public Notification(Guid id, Guid recipientId, Guid actorId, NotificationType type, Guid? referenceId, string message)
    {
        Id = id;
        RecipientId = recipientId;
        ActorId = actorId;
        Type = type;
        ReferenceId = referenceId;
        Message = message;
        IsRead = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsRead() => IsRead = true;
}
