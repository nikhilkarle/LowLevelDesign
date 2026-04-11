namespace RestaurantManagementSystem.Domain.Entities;

public class InvoiceLine
{
    public string Description { get; }
    public decimal Amount { get; }

    public InvoiceLine(string description, decimal amount)
    {
        Description = description;
        Amount = amount;
    }
}

public class Invoice
{
    public Guid Id { get; }
    public Guid OrderId { get; }
    public decimal TotalAmount { get; }
    public DateTime IssuedAt { get; }

    private readonly List<InvoiceLine> _lines;
    public IReadOnlyList<InvoiceLine> Lines => _lines;

    public Invoice(Guid id, Guid orderId, List<InvoiceLine> lines)
    {
        Id = id;
        OrderId = orderId;
        _lines = lines;
        TotalAmount = lines.Sum(l => l.Amount);
        IssuedAt = DateTime.UtcNow;
    }
}
