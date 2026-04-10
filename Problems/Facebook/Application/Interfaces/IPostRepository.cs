using Facebook.Domain.Entities;

namespace Facebook.Application.Interfaces;

public interface IPostRepository
{
    void Add(Post post);
    Post? GetById(Guid id);
    IReadOnlyList<Post> GetByAuthor(Guid authorId);
    IReadOnlyList<Post> GetAllVisibleToUser(IEnumerable<Guid> authorIds);
}
