using System;

namespace AirlineManagementSystem.Application.DTOs;

public record CancelBookingRequest(Guid BookingId, string Reason);