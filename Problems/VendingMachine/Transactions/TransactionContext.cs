using VendingMachine.Domain;

namespace VendingMachine.Transactions;

public sealed class TransactionContext
{
    public Guid Id { get; } = Guid.NewGuid();

    public string? SelectedProductId { get; set; }
    public Money Inserted { get; } = new Money();

    public override string ToString() => $"Tx={Id}, product={SelectedProductId}, inserted={Inserted.TotalCents}c";
}
