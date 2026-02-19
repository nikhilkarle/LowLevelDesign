using StackOverflow.Models;
using StackOverflow.Repositories;

namespace StackOverflow.Search;

public sealed class KeywordSearchStrategy : ISearchStrategy
{
    private readonly IQuestionRepository _questions;
    public KeywordSearchStrategy(IQuestionRepository questions) => _questions = questions;

    public IEnumerable<Question> Search(SearchQuery query)
    {
        if (string.IsNullOrWhiteSpace(query.Keyword)) return Enumerable.Empty<Question>();
        var key = query.Keyword.Trim().ToLowerInvariant();

        return _questions.GetAll().Where(q =>
            q.Content.ToLowerInvariant().Contains(key) ||
            q.Keywords.Any(k => k.ToLowerInvariant().Contains(key)));
    }
}
