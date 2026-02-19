namespace StackOverflow.Search;

public sealed record SearchQuery(string? Keyword, string? Tag, long? UserId);
