using System;
using System.Collections.Generic;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Interfaces;

public interface IFlightRepository
{
    void Add(Flight flight);
    Flight? GetById(Guid id);
    List<Flight> GetAll();
    void Update(Flight flight);
}