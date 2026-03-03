namespace AtmSystem.Hardware;

public sealed record CashPlan(IReadOnlyDictionary<int, int> Bills)
{
    public int TotalDollars => Bills.Sum(kv => kv.Key * kv.Value);
}

public interface IAtmCashStore
{
    CashPlan? TryAllocate(int amountDollars);
    void Commit(CashPlan plan);
    string Snapshot();
}