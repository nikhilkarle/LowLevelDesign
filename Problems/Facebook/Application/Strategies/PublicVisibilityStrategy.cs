using Facebook.Domain.Entities;

namespace Facebook.Application.Strategies;

public class PublicVisibilityStrategy : IVisibilityStrategy
{
    public bool CanView(User viewer, Post post, IEnumerable<Guid> viewerFriendIds) => true;
}
