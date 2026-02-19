using StackOverflow.Models;

namespace StackOverflow.Reputation;

public sealed record UserActivityEvent(
    long ActorUserId,
    ActivityType Type,
    PostType TargetType,
    long TargetId,
    long? TargetOwnerUserId
);
