using System;
using System.Collections.Generic;
using System.Linq;
using AirlineManagementSystem.Application.Interfaces;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Infrastructure.Repositories;

public class InMemoryBookingRepository : IBookingRepository
{
    private readonly List<Booking> _bookings = new();

    public void Add(Booking booking)
    {
        _bookings.Add(booking);
    }

    public Booking? GetById(Guid id)
    {
        return _bookings.FirstOrDefault(b => b.Id == id);
    }

    public List<Booking> GetAll()
    {
        return _bookings;
    }

    public void Update(Booking booking)
    {
    }
}