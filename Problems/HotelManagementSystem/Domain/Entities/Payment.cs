using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.Entities;

public sealed class Payment
{
    public Guid Id { get; }
    public Guid ReservationId { get; }
    public decimal Amount { get; }
    public PaymentMethod Method { get; }
    public PaymentStatus Status { get; private set; }
    public DateTime CreatedAt { get; }

    public Payment(Guid id, Guid reservationId, decimal amount, PaymentMethod method)
    {
        Id = id;
        ReservationId = reservationId;
        Amount = amount;
        Method = method;
        Status = PaymentStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkPaid() => Status = PaymentStatus.Paid;
    public void MarkFailed() => Status = PaymentStatus.Failed;
    public void Refund() => Status = PaymentStatus.Refunded;
}