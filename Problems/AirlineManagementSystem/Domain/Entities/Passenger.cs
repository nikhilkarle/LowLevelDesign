using System;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Domain.Entities;

public class Passenger : User
{
    public string PassportNumber { get; private set; }
    public DateOnly DateOfBirth { get; private set; }

    public Passenger(Guid id, string name, string email, UserRole role, string passportNumber, DateOnly dateOfBirth)
        : base(id, name, email, role)
    {
        PassportNumber = passportNumber;
        DateOfBirth = dateOfBirth;
    }
}