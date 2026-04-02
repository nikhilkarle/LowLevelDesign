using System;
using System.Collections.Generic;
using System.Linq;
using AirlineManagementSystem.Application.Interfaces;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Infrastructure.Repositories;

public class InMemoryFlightRepository : IFlightRepository
{
    private readonly List<Flight> _flights = new();

    public void Add(Flight flight)
    {
        _flights.Add(flight);
    }

    public Flight? GetById(Guid id)
    {
        return _flights.FirstOrDefault(f => f.Id == id);
    }

    public List<Flight> GetAll()
    {
        return _flights;
    }

    public void Update(Flight flight)
    {
    }
}