using Facebook.Application.Interfaces;
using Facebook.Domain.Entities;

namespace Facebook.Infrastructure.Repositories;

public class InMemoryPostRepository : IPostRepository
{
    private readonly Dictionary<Guid, Post> _store = new();

    public void Add(Post post) => _store[post.Id] = post;

    public Post? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Post> GetByAuthor(Guid authorId)
        => _store.Values.Where(p => p.AuthorId == authorId).ToList();

    public IReadOnlyList<Post> GetAllVisibleToUser(IEnumerable<Guid> authorIds)
    {
        var authorSet = authorIds.ToHashSet();
        return _store.Values.Where(p => authorSet.Contains(p.AuthorId)).ToList();
    }
}
