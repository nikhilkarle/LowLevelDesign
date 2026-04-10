using Facebook.Domain.Entities;

namespace Facebook.Domain.Rules;

// Runs every rule in sequence — if any throws, the request is rejected
public class CompositeFriendRequestRule : IFriendRequestRule
{
    private readonly IEnumerable<IFriendRequestRule> _rules;

    public CompositeFriendRequestRule(IEnumerable<IFriendRequestRule> rules)
    {
        _rules = rules;
    }

    public void Validate(User sender, User receiver)
    {
        foreach (var rule in _rules)
            rule.Validate(sender, receiver);
    }
}
