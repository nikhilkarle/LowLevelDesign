using System;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Domain.Entities;

public class User
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public UserRole Role { get; private set; }

    public User(Guid id, string name, string email, UserRole role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
    }
}