using VendingMachine.Domain;

namespace VendingMachine.Strategies;

public interface IChangeStrategy
{
    Dictionary<Denomination,int>? TryMakeChange(int changeCents, IReadOnlyDictionary<Denomination,int> available);
}
