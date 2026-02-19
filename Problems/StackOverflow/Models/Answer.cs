namespace StackOverflow.Models;

public sealed class Answer
{
    public long Id { get; init; }
    public long QuestionId { get; init; }
    public long AuthorId { get; init; }
    public string Content { get; set; } = "";

    public List<string> Keywords { get; } = new();
    public int VoteScore { get; private set; }

    public void ApplyVote(int delta) => VoteScore += delta;
}
