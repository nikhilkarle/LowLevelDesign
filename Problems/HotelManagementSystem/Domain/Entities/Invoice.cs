namespace HotelManagementSystem.Domain.Entities;

public sealed class Invoice
{
    public Guid Id { get; }
    public Guid ReservationId { get; }
    public IReadOnlyList<ChargeItem> Charges => _charges.AsReadOnly();
    public bool IsClosed { get; private set; }

    private readonly List<ChargeItem> _charges = new();

    public Invoice(Guid id, Guid reservationId)
    {
        Id = id;
        ReservationId = reservationId;
    }

    public void AddCharge(string description, decimal amount)
    {
        if (IsClosed)
            throw new InvalidOperationException("Invoice is already closed.");

        _charges.Add(new ChargeItem(Guid.NewGuid(), description, amount));
    }

    public decimal GetTotal() => _charges.Sum(x => x.Amount);

    public void Close()
    {
        IsClosed = true;
    }
}