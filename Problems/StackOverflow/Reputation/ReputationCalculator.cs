using StackOverflow.Repositories;

namespace StackOverflow.Reputation;

public sealed class ReputationCalculator : IReputationCalculator
{
    private readonly IUserRepository _users;

    public ReputationCalculator(IUserRepository users) => _users = users;

    public void OnActivity(UserActivityEvent evt)
    {
        var (userId, delta) = evt.Type switch
        {
            ActivityType.PostQuestion => (evt.ActorUserId, +2),
            ActivityType.PostAnswer   => (evt.ActorUserId, +5),
            ActivityType.PostComment  => (evt.ActorUserId, +1),

            ActivityType.Upvote when evt.TargetOwnerUserId.HasValue
                => (evt.TargetOwnerUserId.Value, +10),

            ActivityType.Downvote when evt.TargetOwnerUserId.HasValue
                => (evt.TargetOwnerUserId.Value, -2),

            _ => (0L, 0)
        };

        if (userId == 0 || delta == 0) return;

        var user = _users.GetById(userId);
        if (user is null) return;

        user.AddReputation(delta);
        _users.Save(user);
    }
}
