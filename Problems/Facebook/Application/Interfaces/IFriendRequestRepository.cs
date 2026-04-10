using Facebook.Domain.Entities;
using Facebook.Domain.Enums;

namespace Facebook.Application.Interfaces;

public interface IFriendRequestRepository
{
    void Add(FriendRequest request);
    FriendRequest? GetById(Guid id);
    FriendRequest? GetPending(Guid senderId, Guid receiverId);
    IReadOnlyList<FriendRequest> GetPendingForUser(Guid receiverId);
}
