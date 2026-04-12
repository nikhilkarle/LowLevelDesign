using System.Collections.Concurrent;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();

    public User? GetById(Guid id) => _users.GetValueOrDefault(id);
    public void Add(User user)    => _users[user.Id] = user;
}
