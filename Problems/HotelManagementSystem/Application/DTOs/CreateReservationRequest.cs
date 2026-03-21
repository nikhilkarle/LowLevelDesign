using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Application.DTOs;

public sealed class CreateReservationRequest
{
    public Guid GuestId { get; init; }
    public RoomType RoomType { get; init; }
    public DateTime CheckInDate { get; init; }
    public DateTime CheckOutDate { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
}