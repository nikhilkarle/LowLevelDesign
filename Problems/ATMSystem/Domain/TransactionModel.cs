namespace AtmSystem.Domain;

public enum TransactionType
{
    BalanceInquiry = 1,
    Withdraw = 2,
    Deposit = 3
}

public sealed record Reservation(string TransactionId, string AccountId, long AmountCents, DateTime CreatedUtc);

public sealed record BankBalanceResponse(bool Success, string? Error, long BalanceCents);

public sealed record ReserveFundsResponse(bool Success, string? Error);

public sealed record FinalizeResponse(bool Success, string? Error);

public sealed record CancelResponse(bool Success, string? Error);

public sealed record DepositResponse(bool Success, string? Error);