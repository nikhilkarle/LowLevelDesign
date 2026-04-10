using Facebook.Domain.Entities;

namespace Facebook.Domain.Rules;

public class SelfRequestRule : IFriendRequestRule
{
    public void Validate(User sender, User receiver)
    {
        if (sender.Id == receiver.Id)
            throw new InvalidOperationException("Cannot send a friend request to yourself.");
    }
}
