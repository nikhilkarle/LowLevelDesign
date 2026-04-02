using System;

namespace AirlineManagementSystem.Application.DTOs;

public record ChangeFlightRequest(Guid BookingId, Guid NewFlightId, string NewSeatNumber);