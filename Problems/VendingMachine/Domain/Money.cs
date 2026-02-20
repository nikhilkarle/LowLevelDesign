using System.Collections.ObjectModel;

namespace VendingMachine.Domain;

public sealed class Money
{
    private readonly Dictionary<Denomination, int> _counts = new();

    public IReadOnlyDictionary<Denomination, int> Counts =>
        new ReadOnlyDictionary<Denomination, int>(_counts);

    public int TotalCents => _counts.Sum(kv => (int)kv.Key*kv.Value);

    public void Add(Denomination denom, int count = 1)
    {
        if (count <= 0) throw new ArgumentException("Count should be greater than 0");
        _counts[denom] = _counts.GetValueOrDefault(denom) + count;
    }

    public void AddRange(IReadOnlyDictionary<Denomination, int> counts)
    {
        foreach (var kv in counts)
        {
            if (kv.Value <= 0) continue;
            Add(kv.Key, kv.Value);
        }
    }

    public Dictionary<Denomination, int> ToMutableCounts()
    {
        return new Dictionary<Denomination, int>(_counts);
    }

}