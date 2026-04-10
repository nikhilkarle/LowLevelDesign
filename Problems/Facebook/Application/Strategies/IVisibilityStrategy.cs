using Facebook.Domain.Entities;

namespace Facebook.Application.Strategies;

// Strategy Pattern: each privacy setting has its own rule for who can see a post
public interface IVisibilityStrategy
{
    bool CanView(User viewer, Post post, IEnumerable<Guid> viewerFriendIds);
}
