using Facebook.Domain.Entities;

namespace Facebook.Domain.Rules;

public class AlreadyFriendsRule : IFriendRequestRule
{
    public void Validate(User sender, User receiver)
    {
        if (sender.IsFriendWith(receiver.Id))
            throw new InvalidOperationException($"{sender.Name} and {receiver.Name} are already friends.");
    }
}
