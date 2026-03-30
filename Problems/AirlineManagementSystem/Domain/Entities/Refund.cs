using System;

namespace AirlineManagementSystem.Domain.Entities;

public class Refund
{
    public Guid Id { get; }
    public Guid BookingId { get; }
    public double Amount { get; }
    public DateTime RefundedAtUtc { get; }
    public string Reason { get; }

    public Refund(Guid id, Guid bookingId, double amount, string reason)
    {
        Id = id;
        BookingId = bookingId;
        Amount = amount;
        Reason = reason;
        RefundedAtUtc = DateTime.UtcNow;
    }
}