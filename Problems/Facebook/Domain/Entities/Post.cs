using Facebook.Domain.Enums;
using Facebook.Domain.ValueObjects;

namespace Facebook.Domain.Entities;

public class Post
{
    public Guid Id { get; }
    public Guid AuthorId { get; }
    public PostType Type { get; }
    public string TextContent { get; private set; }
    public MediaContent? Media { get; private set; }
    public PrivacySetting Privacy { get; private set; }
    public DateTime CreatedAt { get; }

    private readonly List<Guid> _likedByUserIds = new();
    private readonly List<Comment> _comments = new();

    public IReadOnlyList<Guid> LikedByUserIds => _likedByUserIds;
    public IReadOnlyList<Comment> Comments => _comments;

    public Post(Guid id, Guid authorId, PostType type, string textContent, MediaContent? media, PrivacySetting privacy)
    {
        Id = id;
        AuthorId = authorId;
        Type = type;
        TextContent = textContent;
        Media = media;
        Privacy = privacy;
        CreatedAt = DateTime.UtcNow;
    }

    public void Like(Guid userId)
    {
        if (!_likedByUserIds.Contains(userId))
            _likedByUserIds.Add(userId);
    }

    public void Unlike(Guid userId) => _likedByUserIds.Remove(userId);

    public Comment AddComment(Guid commentId, Guid authorId, string text)
    {
        var comment = new Comment(commentId, Id, authorId, text);
        _comments.Add(comment);
        return comment;
    }

    public void SetPrivacy(PrivacySetting privacy) => Privacy = privacy;
}
