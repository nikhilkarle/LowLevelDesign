namespace StackOverflow.Models;

public sealed class Comment
{
    public long Id { get; init; }
    public long AuthorId { get; init; }
    public CommentTarget Target { get; init; } = new(PostType.Question, 0);

    public string Content { get; set; } = "";
    public int VoteScore { get; private set; }

    public void ApplyVote(int delta) => VoteScore += delta;
}
