using Facebook.Domain.Entities;
using Facebook.Domain.Enums;
using Facebook.Domain.ValueObjects;

namespace Facebook.Application.Factories;

public interface IPostFactory
{
    Post CreatePost(Guid authorId, PostType type, string text, MediaContent? media, PrivacySetting privacy);
}
