using VendingMachine.Domain;

namespace VendingMachine.Repositories;

public sealed class InMemoryCashRepository : ICashRepository
{
    private readonly Dictionary<Denomination, int> _box = new();

    public IReadOnlyDictionary<Denomination, int> Snapshot()
        => new Dictionary<Denomination,int>(_box);

    public void AddInserted(IReadOnlyDictionary<Denomination, int> inserted)
    {
        foreach (var kv in inserted)
            _box[kv.Key] = _box.GetValueOrDefault(kv.Key) + kv.Value;
    }

    public bool TryWithdraw(IReadOnlyDictionary<Denomination, int> withdrawal)
    {
        foreach (var kv in withdrawal)
        {
            var have = _box.GetValueOrDefault(kv.Key);
            if (have < kv.Value) return false;
        }

        foreach (var kv in withdrawal)
            _box[kv.Key] -= kv.Value;

        return true;
    }

    public void Restock(IReadOnlyDictionary<Denomination, int> added)
    {
        foreach (var kv in added)
        {
            if (kv.Value <= 0) continue;
            _box[kv.Key] = _box.GetValueOrDefault(kv.Key) + kv.Value;
        }
    }

    public IReadOnlyDictionary<Denomination, int> CollectAll()
    {
        var all = new Dictionary<Denomination,int>(_box);
        _box.Clear();
        return all;
    }
}
