using System;
using System.Collections.Generic;
using AirlineManagementSystem.Domain.Entities;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Application.DTOs;

public record BookFlightRequest(
    Guid PassengerId,
    Guid FlightId,
    string SeatNumber,
    PaymentMethod PaymentMethod,
    double Amount,
    List<Baggage> BaggageItems);