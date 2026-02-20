using VendingMachine.Concurrency;
using VendingMachine.Domain;
using VendingMachine.Events;
using VendingMachine.Repositories;
using VendingMachine.Strategies;
using VendingMachine.Transactions;

namespace VendingMachine.Facade;

public sealed class VendingMachineClass
{
    private readonly IInventoryRepository _inventory;
    private readonly ICashRepository _cash;
    private readonly IChangeStrategy _changeStrategy;
    private readonly IEventPublisher _events;
    private readonly LockProvider _locks;

    private readonly Dictionary<Guid, (TransactionContext Ctx, TransactionState State)> _tx = new();

    public VendingMachineClass(
        IInventoryRepository inventory,
        ICashRepository cash,
        IChangeStrategy changeStrategy,
        IEventPublisher events,
        LockProvider locks)
    {
        _inventory = inventory;
        _cash = cash;
        _changeStrategy = changeStrategy;
        _events = events;
        _locks = locks;
    }

    public Guid StartTransaction()
    {
        var ctx = new TransactionContext();
        _tx[ctx.Id] = (ctx, new IdleState());
        return ctx.Id;
    }

    public void SelectProduct(Guid txId, string productId)
    {
        var (ctx, state) = Get(txId);

        if (state is IdleState)
            _tx[txId] = (ctx, new HasSelectionState());

        (_, state) = Get(txId);
        try
        {
            state.SelectProduct(ctx, productId);
        }
        catch
        {
            throw;
        }
    }

    public void Insert(Guid txId, Denomination denom)
    {
        var (ctx, state) = Get(txId);

        if (state is HasSelectionState)
            _tx[txId] = (ctx, new CollectingMoneyState());

        (_, state) = Get(txId);
        try
        {
            state.Insert(ctx, denom);
        }
        catch
        {
            throw;
        }
    }

    public VendingResult Cancel(Guid txId)
    {
        var (ctx, state) = Get(txId);

        var refund = ctx.Inserted.ToMutableCounts();

        try { state.Cancel(ctx); } catch { }

        _tx.Remove(txId);
        return VendingResult.Ok(productId: "REFUND", change: refund);
    }

    public VendingResult Complete(Guid txId)
    {
        var (ctx, _) = Get(txId);

        if (string.IsNullOrWhiteSpace(ctx.SelectedProductId))
        {
            _tx.Remove(txId);
            return VendingResult.Fail(new TransactionStateError("No product selected"));
        }

        var productId = ctx.SelectedProductId!;
        var insertedCents = ctx.Inserted.TotalCents;

        var productLock = _locks.ForProduct(productId);
        lock (productLock)
        {
            lock (_locks.CashLock)
            {
                var product = _inventory.GetById(productId);
                if (product is null)
                {
                    _tx.Remove(txId);
                    return VendingResult.Fail(new InvalidProductError(productId));
                }

                if (!product.IsInStock)
                {
                    _events.Publish(new ProductOutOfStock(productId));
                    _tx.Remove(txId);
                    return VendingResult.Fail(new OutOfStockError(productId));
                }

                if (insertedCents < product.PriceCents)
                {
                    _tx.Remove(txId);
                    return VendingResult.Fail(new InsufficientFundsError(product.PriceCents, insertedCents));
                }

                var changeCents = insertedCents - product.PriceCents;

                var available = _cash.Snapshot();

                var availablePlusInserted = new Dictionary<Denomination,int>(available);
                foreach (var kv in ctx.Inserted.Counts)
                    availablePlusInserted[kv.Key] = availablePlusInserted.GetValueOrDefault(kv.Key) + kv.Value;

                var changeMap = _changeStrategy.TryMakeChange(changeCents, availablePlusInserted);
                if (changeMap is null)
                {
                    _tx.Remove(txId);
                    return VendingResult.Fail(new CannotMakeChangeError(changeCents));
                }

                _cash.AddInserted(ctx.Inserted.Counts);

                _inventory.RemoveOne(productId);

                var ok = _cash.TryWithdraw(changeMap);
                if (!ok)
                {
                    _tx.Remove(txId);
                    return VendingResult.Fail(new CannotMakeChangeError(changeCents));
                }

                _events.Publish(new ProductDispensed(productId));

                _tx.Remove(txId);
                return VendingResult.Ok(productId, changeMap);
            }
        }
    }


    public void AdminRestockProduct(string productId, int addedQuantity)
    {
        var productLock = _locks.ForProduct(productId);
        lock (productLock)
        {
            _inventory.AddStock(productId, addedQuantity);
            _events.Publish(new ProductRestocked(productId, addedQuantity));
        }
    }

    public IReadOnlyDictionary<Denomination,int> AdminCollectCash()
    {
        lock (_locks.CashLock)
        {
            var collected = _cash.CollectAll();
            var total = collected.Sum(kv => (int)kv.Key * kv.Value);
            _events.Publish(new CashCollected(total));
            return collected;
        }
    }

    public void AdminRestockCash(IReadOnlyDictionary<Denomination,int> added)
    {
        lock (_locks.CashLock)
        {
            _cash.Restock(added);
        }
    }

    private (TransactionContext Ctx, TransactionState State) Get(Guid txId)
    {
        if (!_tx.TryGetValue(txId, out var tuple))
            throw new InvalidOperationException($"Unknown transaction {txId}");
        return tuple;
    }
}
