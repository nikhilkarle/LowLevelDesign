using Facebook.Application.Interfaces;
using Facebook.Domain.Entities;

namespace Facebook.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<Guid, User> _store = new();

    public void Add(User user) => _store[user.Id] = user;

    public User? GetById(Guid id) => _store.GetValueOrDefault(id);

    public User? GetByEmail(string email)
        => _store.Values.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<User> GetAll() => _store.Values.ToList();
}
