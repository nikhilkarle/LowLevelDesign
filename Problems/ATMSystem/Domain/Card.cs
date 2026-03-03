namespace AtmSystem.Domain;

public sealed record Card(string CardNumber, string LinkedAccountId, bool IsActive, DateTime ExpiresUtc)
{
    public bool IsExpiredUtc(DateTime utcNow) => utcNow > ExpiresUtc;
}