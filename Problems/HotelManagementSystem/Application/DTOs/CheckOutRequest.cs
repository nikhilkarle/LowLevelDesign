using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Application.DTOs;

public sealed class CheckOutRequest
{
    public Guid ReservationId { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
}