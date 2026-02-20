namespace VendingMachine.Transactions;

public sealed class IdleState : TransactionState
{
    public override void SelectProduct(TransactionContext ctx, string productId)
    {
        ctx.SelectedProductId = productId;
    }
}
