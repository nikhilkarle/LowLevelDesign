using StackOverflow.Models;

namespace StackOverflow.Repositories;

public sealed class InMemoryCommentRepository : ICommentRepository
{
    private readonly Dictionary<long, Comment> _store = new();

    public Comment? GetById(long id) => _store.TryGetValue(id, out var u) ? u : null;
    public IEnumerable<Comment> GetAll() => _store.Values;
    public void Save(Comment comment) => _store[comment.Id] = comment;
} 