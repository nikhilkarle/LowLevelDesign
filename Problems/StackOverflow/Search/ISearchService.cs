using StackOverflow.Models;

namespace StackOverflow.Search;

public interface ISearchService
{
    IEnumerable<Question> Search(SearchQuery query);
}
