using System.Collections.Generic;
using AirlineManagementSystem.Application.DTOs;
using AirlineManagementSystem.Domain.Entities;

namespace AirlineManagementSystem.Application.Interfaces;

public interface IFlightService
{
    List<Flight> SearchFlights(FlightSearchRequest request);
}