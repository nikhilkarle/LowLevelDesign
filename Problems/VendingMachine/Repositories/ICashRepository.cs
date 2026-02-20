using VendingMachine.Domain;

namespace VendingMachine.Repositories;

public interface ICashRepository
{
    IReadOnlyDictionary<Denomination,int> Snapshot();

    void AddInserted(IReadOnlyDictionary<Denomination,int> inserted);
    bool TryWithdraw(IReadOnlyDictionary<Denomination,int> withdrawal);

    void Restock(IReadOnlyDictionary<Denomination,int> added);
    IReadOnlyDictionary<Denomination,int> CollectAll();
}
