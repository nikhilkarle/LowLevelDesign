using StackOverflow.Models;

namespace StackOverflow.Search;

public interface ISearchStrategy
{
    IEnumerable<Question> Search(SearchQuery query);
}
