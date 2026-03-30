using System;

namespace AirlineManagementSystem.Application.DTOs;

public record FlightSearchRequest(string Source, string Destination, DateOnly Date);