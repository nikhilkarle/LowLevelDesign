using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.Entities;

public class Payment
{
    public Guid Id { get; }
    public Guid InvoiceId { get; }
    public decimal Amount { get; }
    public PaymentMethod Method { get; }
    public PaymentStatus Status { get; private set; }
    public DateTime ProcessedAt { get; }

    public Payment(Guid id, Guid invoiceId, decimal amount, PaymentMethod method)
    {
        Id = id;
        InvoiceId = invoiceId;
        Amount = amount;
        Method = method;
        Status = PaymentStatus.Pending;
        ProcessedAt = DateTime.UtcNow;
    }

    public void Complete() => Status = PaymentStatus.Completed;
    public void Fail()    => Status = PaymentStatus.Failed;
    public void Refund()  => Status = PaymentStatus.Refunded;
}
