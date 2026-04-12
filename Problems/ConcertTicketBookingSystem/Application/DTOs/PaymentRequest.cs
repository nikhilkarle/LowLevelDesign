using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Application.DTOs;

public class PaymentRequest
{
    public required Guid          BookingId { get; init; }
    public required decimal       Amount    { get; init; }
    public required PaymentMethod Method    { get; init; }
}

public class PaymentResult
{
    public bool    Success       { get; set; }
    public string? TransactionId { get; set; }
    public string? ErrorMessage  { get; set; }
}
