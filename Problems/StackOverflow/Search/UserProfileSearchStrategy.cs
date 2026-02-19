using StackOverflow.Models;
using StackOverflow.Repositories;

namespace StackOverflow.Search;

public sealed class UserProfileSearchStrategy : ISearchStrategy
{
    private readonly IQuestionRepository _questions;
    public UserProfileSearchStrategy(IQuestionRepository questions) => _questions = questions;

    public IEnumerable<Question> Search(SearchQuery query)
    {
        if (!query.UserId.HasValue) return Enumerable.Empty<Question>();
        return _questions.GetAll().Where(q => q.AuthorId == query.UserId.Value);
    }
}
