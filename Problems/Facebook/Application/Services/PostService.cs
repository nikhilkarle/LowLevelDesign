using Facebook.Application.Factories;
using Facebook.Application.Interfaces;
using Facebook.Application.Strategies;
using Facebook.Domain.Entities;
using Facebook.Domain.Enums;
using Facebook.Domain.ValueObjects;

namespace Facebook.Application.Services;

public class PostService
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPostFactory _postFactory;
    private readonly NotificationService _notificationService;

    private readonly Dictionary<PrivacySetting, IVisibilityStrategy> _visibilityStrategies;

    public PostService(
        IPostRepository postRepository,
        IUserRepository userRepository,
        IPostFactory postFactory,
        NotificationService notificationService,
        Dictionary<PrivacySetting, IVisibilityStrategy> visibilityStrategies)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
        _postFactory = postFactory;
        _notificationService = notificationService;
        _visibilityStrategies = visibilityStrategies;
    }

    public Post CreatePost(Guid authorId, PostType type, string text, PrivacySetting privacy, MediaContent? media = null)
    {
        var post = _postFactory.CreatePost(authorId, type, text, media, privacy);
        _postRepository.Add(post);
        return post;
    }

    public void LikePost(Guid likerId, Guid postId)
    {
        var post = GetOrThrow(postId);
        var liker = GetUserOrThrow(likerId);

        EnforceVisibility(liker, post);

        post.Like(likerId);

        if (post.AuthorId != likerId)
            _notificationService.SendLikeNotification(likerId, post.AuthorId, postId, liker.Name);
    }

    public void UnlikePost(Guid userId, Guid postId)
    {
        var post = GetOrThrow(postId);
        post.Unlike(userId);
    }

    public Comment AddComment(Guid authorId, Guid postId, string text)
    {
        var post = GetOrThrow(postId);
        var commenter = GetUserOrThrow(authorId);

        EnforceVisibility(commenter, post);

        var comment = post.AddComment(Guid.NewGuid(), authorId, text);

        if (post.AuthorId != authorId)
            _notificationService.SendCommentNotification(authorId, post.AuthorId, postId, commenter.Name);

        return comment;
    }

    public Post GetOrThrow(Guid postId)
        => _postRepository.GetById(postId)
           ?? throw new InvalidOperationException($"Post {postId} not found.");

    private User GetUserOrThrow(Guid userId)
        => _userRepository.GetById(userId)
           ?? throw new InvalidOperationException($"User {userId} not found.");

    private void EnforceVisibility(User viewer, Post post)
    {
        var strategy = _visibilityStrategies[post.Privacy];
        if (!strategy.CanView(viewer, post, viewer.FriendIds))
            throw new UnauthorizedAccessException("You do not have permission to interact with this post.");
    }
}
