using Facebook.Domain.Entities;

namespace Facebook.Application.Strategies;

// Strategy Pattern: swap sort algorithm without changing NewsfeedService
public interface INewsfeedStrategy
{
    IReadOnlyList<Post> Sort(IEnumerable<Post> posts);
}
