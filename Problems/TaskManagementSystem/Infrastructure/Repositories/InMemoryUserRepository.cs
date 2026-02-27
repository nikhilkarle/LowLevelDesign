using System.Collections.Concurrent;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructure.Repositories;

public sealed class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();

    public User? GetById(Guid userId)
    {
        _users.TryGetValue(userId, out User? user);
        return user;
    }

    public void Add(User user)
    {
        if (!_users.TryAdd(user.Id, user))
            throw new InvalidOperationException("User already exists.");
    }
}
