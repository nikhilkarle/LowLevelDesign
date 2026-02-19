using StackOverflow.Models;
using StackOverflow.Services;

namespace StackOverflow.Factories;

public sealed class AnswerFactory : IAnswerFactory
{
    public Answer Create(long authorId, long questionId, string content, IEnumerable<string> keywords)
    {
        var a = new Answer
        {
            Id = IdGen.NewId(),
            AuthorId = authorId,
            QuestionId = questionId,
            Content = (content ?? "").Trim()
        };

        foreach (var k in Normalize(keywords))
            a.Keywords.Add(k);

        return a;
    }

    private static IEnumerable<string> Normalize(IEnumerable<string> values) =>
        values
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Select(v => v.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase);
}
