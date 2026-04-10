namespace Facebook.Domain.ValueObjects;

public sealed record MediaContent(string Url, string MimeType, long SizeInBytes);
