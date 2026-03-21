using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Application.DTOs;

public sealed class PaymentRequest
{
    public Guid ReservationId { get; init; }
    public decimal Amount { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
}