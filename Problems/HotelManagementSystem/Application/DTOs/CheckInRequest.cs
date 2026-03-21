namespace HotelManagementSystem.Application.DTOs;

public sealed class CheckInRequest
{
    public Guid ReservationId { get; init; }
}