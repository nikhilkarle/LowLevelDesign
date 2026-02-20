namespace VendingMachine.Domain;

public sealed record VendingResult(
    bool Success,
    string? DispensedProductId,
    IReadOnlyDictionary<Denomination,int> Change,
    VendingError? Error)
{
    public static VendingResult Ok(string productId, IReadOnlyDictionary<Denomination,int> change)
        => new(true, productId, change, null);

    public static VendingResult Fail(VendingError err)
        => new(false, null, new Dictionary<Denomination,int>(), err);

    public override string ToString()
    {
        if (!Success) return $"FAIL: {Error?.Message}";
        var changeStr = Change.Count == 0 ? "no change" : string.Join(", ", Change.Select(k => $"{k.Key}:{k.Value}"));
        return $"OK: dispensed={DispensedProductId}, change={changeStr}";
    }
}
