using OAS.Domain.Entities;

namespace OAS.Infrastructure.Repositories;
public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<Guid, User> _users = new();

    public void Add(User user) => _users[user.Id] = user;

    public User? GetById(Guid id) => _users.TryGetValue(id, out var user) ? user : null;

    public User? GetByEmail(string email) =>
        _users.Values.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
}