using StackOverflow.Models;

namespace StackOverflow.Search;

public sealed class SearchService : ISearchService
{
    private readonly ISearchStrategy _keyword;
    private readonly ISearchStrategy _tag;
    private readonly ISearchStrategy _user;

    public SearchService(ISearchStrategy keyword, ISearchStrategy tag, ISearchStrategy user)
    {
        _keyword = keyword;
        _tag = tag;
        _user = user;
    }

    public IEnumerable<Question> Search(SearchQuery query)
    {
        if (!string.IsNullOrWhiteSpace(query.Tag)) return _tag.Search(query);
        if (query.UserId.HasValue) return _user.Search(query);
        return _keyword.Search(query);
    }
}
