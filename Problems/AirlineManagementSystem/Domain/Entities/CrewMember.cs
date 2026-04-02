using System;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Domain.Entities;

public class CrewMember
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public CrewRole Role { get; private set; }

    public CrewMember(Guid id, string name, CrewRole role)
    {
        Id = id;
        Name = name;
        Role = role;
    }
}