namespace AtmSystem.Domain;

public sealed record Result(bool Success, string? Error)
{
    public static Result Ok() => new(true, null);
    public static Result Fail(string error) => new(false, error);
}

public sealed record AuthResult(bool Success, string? Error, string? AccountId)
{
    public static AuthResult Ok(string accountId) => new(true, null, accountId);
    public static AuthResult Fail(string error) => new(false, error, null);
}

public sealed record TransactionResult(bool Success, string? Error, string? Receipt)
{
    public static TransactionResult Ok(string? receipt = null) => new(true, null, receipt);
    public static TransactionResult Fail(string error) => new(false, error, null);
}