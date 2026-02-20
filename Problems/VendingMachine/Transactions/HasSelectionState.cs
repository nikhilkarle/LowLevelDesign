using VendingMachine.Domain;

namespace VendingMachine.Transactions;

public sealed class HasSelectionState : TransactionState
{
    public override void Insert(TransactionContext ctx, Denomination denom)
    {
        ctx.Inserted.Add(denom);
    }

    public override void SelectProduct(TransactionContext ctx, string productId)
    {
        ctx.SelectedProductId = productId;
    }

    public override void Cancel(TransactionContext ctx)
    {
        ctx.SelectedProductId = null;
    }
}
