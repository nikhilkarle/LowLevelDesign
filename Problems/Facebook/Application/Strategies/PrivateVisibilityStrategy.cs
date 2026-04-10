using Facebook.Domain.Entities;

namespace Facebook.Application.Strategies;

public class PrivateVisibilityStrategy : IVisibilityStrategy
{
    public bool CanView(User viewer, Post post, IEnumerable<Guid> viewerFriendIds)
        => post.AuthorId == viewer.Id;
}
