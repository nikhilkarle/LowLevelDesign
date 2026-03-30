using System;
using System.Collections.Generic;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Interfaces;

public interface IBookingRepository
{
    void Add(Booking booking);
    Booking? GetById(Guid id);
    List<Booking> GetAll();
    void Update(Booking booking);
}