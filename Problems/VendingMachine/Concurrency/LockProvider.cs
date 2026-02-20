using System.Collections.Concurrent;

namespace VendingMachine.Concurrency;

public sealed class LockProvider
{
    private readonly ConcurrentDictionary<string, object> _locks = new();
    private readonly object _cashLock = new();

    public object ForProduct(string productId) => _locks.GetOrAdd(productId, _ => new object());
    public object CashLock => _cashLock;
}
