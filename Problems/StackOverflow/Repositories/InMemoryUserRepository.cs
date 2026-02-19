using StackOverflow.Models;

namespace StackOverflow.Repositories;

public sealed class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<long, User> _store = new();

    public User? GetById(long id) => _store.TryGetValue(id, out var u) ? u : null;
    public IEnumerable<User> GetAll() => _store.Values;
    public void Save(User user) => _store[user.Id] = user;
} 