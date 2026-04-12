using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Application.DTOs;

public class BookingRequest
{
    public required Guid          UserId        { get; init; }
    public required Guid          ConcertId     { get; init; }
    public required List<Guid>    SeatIds       { get; init; }
    public required PaymentMethod PaymentMethod { get; init; }
}
