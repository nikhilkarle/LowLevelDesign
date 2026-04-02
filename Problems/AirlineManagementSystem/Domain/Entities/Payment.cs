using System;
using AirlineManagementSystem.Domain.Enums;

namespace AirlineManagementSystem.Domain.Entities;

public class Payment
{
    public Guid Id { get; }
    public Guid BookingId { get; }
    public double Amount { get; }
    public PaymentMethod PaymentMethod { get; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; }

    public Payment(Guid id, Guid bookingId, double amount, PaymentMethod paymentMethod)
    {
        Id = id;
        BookingId = bookingId;
        Amount = amount;
        PaymentMethod = paymentMethod;
        Status = PaymentStatus.Pending;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void MarkSuccess() => Status = PaymentStatus.Success;
    public void MarkFailed() => Status = PaymentStatus.Failed;
    public void MarkRefunded() => Status = PaymentStatus.Refunded;
}