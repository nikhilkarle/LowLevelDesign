using Facebook.Application.Interfaces;
using Facebook.Domain.Entities;
using Facebook.Domain.Rules;

namespace Facebook.Application.Services;

public class FriendService
{
    private readonly IUserRepository _userRepository;
    private readonly IFriendRequestRepository _friendRequestRepository;
    private readonly NotificationService _notificationService;
    private readonly IFriendRequestRule _validationChain;

    public FriendService(
        IUserRepository userRepository,
        IFriendRequestRepository friendRequestRepository,
        NotificationService notificationService,
        IFriendRequestRule validationChain)
    {
        _userRepository = userRepository;
        _friendRequestRepository = friendRequestRepository;
        _notificationService = notificationService;
        _validationChain = validationChain;
    }

    public FriendRequest SendRequest(Guid senderId, Guid receiverId)
    {
        var sender = GetOrThrow(senderId);
        var receiver = GetOrThrow(receiverId);

        _validationChain.Validate(sender, receiver);

        if (_friendRequestRepository.GetPending(senderId, receiverId) is not null)
            throw new InvalidOperationException("A pending friend request already exists.");

        var request = new FriendRequest(Guid.NewGuid(), senderId, receiverId);
        _friendRequestRepository.Add(request);

        _notificationService.SendFriendRequestNotification(senderId, receiverId, request.Id, sender.Name);

        return request;
    }

    public void AcceptRequest(Guid requestId, Guid receiverId)
    {
        var request = _friendRequestRepository.GetById(requestId)
            ?? throw new InvalidOperationException("Friend request not found.");

        if (request.ReceiverId != receiverId)
            throw new InvalidOperationException("Not authorized to accept this request.");

        request.Accept();

        var sender = GetOrThrow(request.SenderId);
        var receiver = GetOrThrow(request.ReceiverId);

        sender.AddFriend(receiverId);
        receiver.AddFriend(request.SenderId);

        _notificationService.SendFriendRequestAcceptedNotification(receiverId, request.SenderId, receiver.Name);
    }

    public void DeclineRequest(Guid requestId, Guid receiverId)
    {
        var request = _friendRequestRepository.GetById(requestId)
            ?? throw new InvalidOperationException("Friend request not found.");

        if (request.ReceiverId != receiverId)
            throw new InvalidOperationException("Not authorized to decline this request.");

        request.Decline();
    }

    public IReadOnlyList<FriendRequest> GetPendingRequests(Guid userId)
        => _friendRequestRepository.GetPendingForUser(userId);

    public IReadOnlyList<User> GetFriends(Guid userId)
    {
        var user = GetOrThrow(userId);
        return user.FriendIds.Select(id => GetOrThrow(id)).ToList();
    }

    private User GetOrThrow(Guid userId)
        => _userRepository.GetById(userId)
           ?? throw new InvalidOperationException($"User {userId} not found.");
}
