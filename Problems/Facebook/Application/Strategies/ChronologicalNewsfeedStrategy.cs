using Facebook.Domain.Entities;

namespace Facebook.Application.Strategies;

public class ChronologicalNewsfeedStrategy : INewsfeedStrategy
{
    public IReadOnlyList<Post> Sort(IEnumerable<Post> posts)
        => posts.OrderByDescending(p => p.CreatedAt).ToList();
}
