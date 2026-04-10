using Facebook.Domain.Entities;

namespace Facebook.Application.Strategies;

public class PopularityNewsfeedStrategy : INewsfeedStrategy
{
    public IReadOnlyList<Post> Sort(IEnumerable<Post> posts)
        => posts.OrderByDescending(p => p.LikedByUserIds.Count + p.Comments.Count).ToList();
}
