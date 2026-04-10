using Facebook.Domain.Entities;

namespace Facebook.Domain.Rules;

public interface IFriendRequestRule
{
    void Validate(User sender, User receiver);
}
