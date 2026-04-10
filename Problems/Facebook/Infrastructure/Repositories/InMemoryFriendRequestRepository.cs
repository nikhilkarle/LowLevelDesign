using Facebook.Application.Interfaces;
using Facebook.Domain.Entities;
using Facebook.Domain.Enums;

namespace Facebook.Infrastructure.Repositories;

public class InMemoryFriendRequestRepository : IFriendRequestRepository
{
    private readonly Dictionary<Guid, FriendRequest> _store = new();

    public void Add(FriendRequest request) => _store[request.Id] = request;

    public FriendRequest? GetById(Guid id) => _store.GetValueOrDefault(id);

    public FriendRequest? GetPending(Guid senderId, Guid receiverId)
        => _store.Values.FirstOrDefault(r =>
            r.SenderId == senderId &&
            r.ReceiverId == receiverId &&
            r.Status == FriendRequestStatus.Pending);

    public IReadOnlyList<FriendRequest> GetPendingForUser(Guid receiverId)
        => _store.Values
            .Where(r => r.ReceiverId == receiverId && r.Status == FriendRequestStatus.Pending)
            .ToList();
}
