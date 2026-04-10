using Facebook.Application.Interfaces;
using Facebook.Application.Strategies;
using Facebook.Domain.Entities;
using Facebook.Domain.Enums;

namespace Facebook.Application.Services;

public class NewsfeedService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly Dictionary<PrivacySetting, IVisibilityStrategy> _visibilityStrategies;

    private INewsfeedStrategy _sortStrategy;

    public NewsfeedService(
        IPostRepository postRepository,
        IUserRepository userRepository,
        Dictionary<PrivacySetting, IVisibilityStrategy> visibilityStrategies,
        INewsfeedStrategy sortStrategy)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _visibilityStrategies = visibilityStrategies;
        _sortStrategy = sortStrategy;
    }

    public void SetSortStrategy(INewsfeedStrategy strategy) => _sortStrategy = strategy;

    public IReadOnlyList<Post> GetNewsfeed(Guid userId)
    {
        var viewer = _userRepository.GetById(userId)
            ?? throw new InvalidOperationException($"User {userId} not found.");

        var authorIds = viewer.FriendIds.Append(userId);
        var candidatePosts = _postRepository.GetAllVisibleToUser(authorIds);

        var visiblePosts = candidatePosts.Where(post =>
        {
            var strategy = _visibilityStrategies[post.Privacy];
            return strategy.CanView(viewer, post, viewer.FriendIds);
        });

        return _sortStrategy.Sort(visiblePosts);
    }
}
