using VendingMachine.Domain;

namespace VendingMachine.Transactions;

public abstract class TransactionState
{
    public virtual void SelectProduct(TransactionContext ctx, string productId)
        => throw new InvalidOperationException("SelectProduct not allowed");

    public virtual void Insert(TransactionContext ctx, Denomination denom)
        => throw new InvalidOperationException("Insert not allowed");

    public virtual void Cancel(TransactionContext ctx)
        => throw new InvalidOperationException("Cancel not allowed");
}
