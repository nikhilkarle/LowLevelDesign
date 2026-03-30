using System;
using System.Collections.Generic;
using System.Linq;
using AirlineManagementSystem.Application.Interfaces;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetById(Guid id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public List<User> GetAll()
    {
        return _users;
    }
}