using Facebook.Domain.Entities;
using Facebook.Domain.Enums;
using Facebook.Domain.ValueObjects;

namespace Facebook.Application.Factories;

public class PostFactory : IPostFactory
{
    public Post CreatePost(Guid authorId, PostType type, string text, MediaContent? media, PrivacySetting privacy)
        => type switch
        {
            PostType.Text  => new(Guid.NewGuid(), authorId, PostType.Text, text, null, privacy),
            PostType.Image => new(Guid.NewGuid(), authorId, PostType.Image, text, media ?? throw new ArgumentNullException(nameof(media), "Image post requires media."), privacy),
            PostType.Video => new(Guid.NewGuid(), authorId, PostType.Video, text, media ?? throw new ArgumentNullException(nameof(media), "Video post requires media."), privacy),
            _              => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown post type.")
        };
}
