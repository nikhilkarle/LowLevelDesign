using StackOverflow.Models;
using StackOverflow.Services;

namespace StackOverflow.Factories;

public sealed class QuestionFactory : IQuestionFactory
{
    public Question Create(long authorId, string content, IEnumerable<string> tags, IEnumerable<string> keywords)
    {
        var q = new Question
        {
            Id = IdGen.NewId(),
            AuthorId = authorId,
            Content = (content ?? "").Trim()
        };

        foreach (var t in Normalize(tags))
            q.Tags.Add(new Tag { Name = t });

        foreach (var k in Normalize(keywords))
            q.Keywords.Add(k);

        return q;
    }

    private static IEnumerable<string> Normalize(IEnumerable<string> values) =>
        values
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Select(v => v.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase);
}
