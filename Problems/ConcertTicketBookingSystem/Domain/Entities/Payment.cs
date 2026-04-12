using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Domain.Entities;

public class Payment
{
    public Guid          Id            { get; }
    public Guid          BookingId     { get; }
    public decimal       Amount        { get; }
    public PaymentMethod Method        { get; }
    public PaymentStatus Status        { get; private set; }
    public string?       TransactionId { get; private set; }

    public Payment(Guid id, Guid bookingId, decimal amount, PaymentMethod method)
    {
        Id = id; BookingId = bookingId; Amount = amount; Method = method;
        Status = PaymentStatus.Pending;
    }

    public void Complete(string transactionId)
    {
        TransactionId = transactionId;
        Status        = PaymentStatus.Completed;
    }

    public void Fail() => Status = PaymentStatus.Failed;
}
