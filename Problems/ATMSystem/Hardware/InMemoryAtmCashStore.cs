using System.Collections.Concurrent;

namespace AtmSystem.Hardware;

public sealed class InMemoryAtmCashStore : IAtmCashStore
{
    private readonly ConcurrentDictionary<int, int> _counts;
    private readonly object _sync = new();

    public InMemoryAtmCashStore(IDictionary<int, int> initial)
    {
        _counts = new ConcurrentDictionary<int, int>(initial);
    }

    public CashPlan? TryAllocate(int amountDollars)
    {
        if (amountDollars <= 0) return null;

        lock (_sync)
        {
            var denoms = _counts.Keys.OrderByDescending(x => x).ToArray();
            var remaining = amountDollars;
            var plan = new Dictionary<int, int>();

            foreach (var d in denoms)
            {
                var available = _counts[d];
                if (available <= 0) continue;

                var take = Math.Min(available, remaining / d);
                if (take > 0)
                {
                    plan[d] = take;
                    remaining -= d * take;
                }

                if (remaining == 0) break;
            }

            if (remaining != 0) return null;
            return new CashPlan(plan);
        }
    }

    public void Commit(CashPlan plan)
    {
        lock (_sync)
        {
            foreach (var (denom, count) in plan.Bills)
            {
                if (!_counts.TryGetValue(denom, out var have) || have < count)
                    throw new InvalidOperationException("Cash store commit mismatch.");

                _counts[denom] = have - count;
            }
        }
    }

    public string Snapshot()
    {
        lock (_sync)
        {
            var parts = _counts
                .OrderByDescending(k => k.Key)
                .Select(k => $"{k.Key}:{k.Value}");
            return string.Join(", ", parts);
        }
    }
}