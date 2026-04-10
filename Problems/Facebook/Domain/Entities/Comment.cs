namespace Facebook.Domain.Entities;

public class Comment
{
    public Guid Id { get; }
    public Guid PostId { get; }
    public Guid AuthorId { get; }
    public string Text { get; private set; }
    public DateTime CreatedAt { get; }

    public Comment(Guid id, Guid postId, Guid authorId, string text)
    {
        Id = id;
        PostId = postId;
        AuthorId = authorId;
        Text = text;
        CreatedAt = DateTime.UtcNow;
    }
}
