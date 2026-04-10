using Facebook.Domain.Entities;

namespace Facebook.Domain.Rules;

public class BlockedUserRule : IFriendRequestRule
{
    public void Validate(User sender, User receiver)
    {
        if (receiver.HasBlocked(sender.Id))
            throw new InvalidOperationException("Cannot send a friend request to this user.");

        if (sender.HasBlocked(receiver.Id))
            throw new InvalidOperationException("Unblock the user before sending a friend request.");
    }
}
