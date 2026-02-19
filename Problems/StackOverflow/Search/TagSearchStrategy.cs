using StackOverflow.Models;
using StackOverflow.Repositories;

namespace StackOverflow.Search;

public sealed class TagSearchStrategy : ISearchStrategy
{
    private readonly IQuestionRepository _questions;
    public TagSearchStrategy(IQuestionRepository questions) => _questions = questions;

    public IEnumerable<Question> Search(SearchQuery query)
    {
        if (string.IsNullOrWhiteSpace(query.Tag)) return Enumerable.Empty<Question>();
        var tag = query.Tag.Trim().ToLowerInvariant();

        return _questions.GetAll().Where(q => q.Tags.Any(t => t.Name.ToLowerInvariant() == tag));
    }
}
