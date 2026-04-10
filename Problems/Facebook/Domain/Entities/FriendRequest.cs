using Facebook.Domain.Enums;

namespace Facebook.Domain.Entities;

public class FriendRequest
{
    public Guid Id { get; }
    public Guid SenderId { get; }
    public Guid ReceiverId { get; }
    public FriendRequestStatus Status { get; private set; }
    public DateTime SentAt { get; }
    public DateTime? RespondedAt { get; private set; }

    public FriendRequest(Guid id, Guid senderId, Guid receiverId)
    {
        Id = id;
        SenderId = senderId;
        ReceiverId = receiverId;
        Status = FriendRequestStatus.Pending;
        SentAt = DateTime.UtcNow;
    }

    public void Accept()
    {
        Status = FriendRequestStatus.Accepted;
        RespondedAt = DateTime.UtcNow;
    }

    public void Decline()
    {
        Status = FriendRequestStatus.Declined;
        RespondedAt = DateTime.UtcNow;
    }
}
