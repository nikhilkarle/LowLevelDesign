using AtmSystem.Domain;

namespace AtmSystem.Bank;

public sealed record CardInfoResponse(bool Success, string? Error, Card? Card);

public sealed record PinVerifyResponse(bool Success, string? Error, string? AccountId);

public interface IBankGateway
{
    Task<CardInfoResponse> GetCardInfoAsync(string cardNumber, CancellationToken ct);

    Task<PinVerifyResponse> VerifyPinAsync(string cardNumber, string pin, CancellationToken ct);

    Task<BankBalanceResponse> GetBalanceAsync(string accountId, CancellationToken ct);

    Task<ReserveFundsResponse> ReserveFundsAsync(string accountId, long amountCents, string transactionId, CancellationToken ct);

    Task<FinalizeResponse> FinalizeWithdrawalAsync(string transactionId, CancellationToken ct);

    Task<CancelResponse> CancelReservationAsync(string transactionId, CancellationToken ct);

    Task<DepositResponse> PostDepositAsync(string accountId, long amountCents, string transactionId, CancellationToken ct);
}