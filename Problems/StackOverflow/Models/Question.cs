namespace StackOverflow.Models;

public sealed class Question
{
    public long Id { get; init; }
    public long AuthorId { get; init; }
    public string Content { get; set; } = "";

    public List<Tag> Tags { get; } = new();
    public List<string> Keywords { get; } = new();
    public int VoteScore { get; private set; }

    public void ApplyVote(int delta) => VoteScore += delta;
}
