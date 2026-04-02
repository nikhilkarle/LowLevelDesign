using System;
using System.Collections.Generic;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Interfaces;

public interface IUserRepository
{
    void Add(User user);
    User? GetById(Guid id);
    List<User> GetAll();
}