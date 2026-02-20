using VendingMachine.Domain;

namespace VendingMachine.Strategies;

public sealed class GreedyBoundedChangeStrategy : IChangeStrategy
{
    public Dictionary<Denomination, int>? TryMakeChange(int changeCents, IReadOnlyDictionary<Denomination, int> available)
    {
        if (changeCents < 0) return null;
        if (changeCents == 0) return new Dictionary<Denomination,int>();

        var result = new Dictionary<Denomination,int>();
        var remaining = changeCents;

        foreach (var denom in Enum.GetValues<Denomination>().OrderByDescending(d => (int)d))
        {
            if (remaining <= 0) break;

            var value = (int)denom;
            var have = available.GetValueOrDefault(denom);
            if (have <= 0 || value > remaining) continue;

            var need = remaining / value;
            var take = Math.Min(need, have);

            if (take > 0)
            {
                result[denom] = take;
                remaining -= take * value;
            }
        }

        return remaining == 0 ? result : null;
    }
}
