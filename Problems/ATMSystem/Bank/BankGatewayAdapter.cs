using AtmSystem.Domain;

namespace AtmSystem.Bank;

public sealed class BankGatewayAdapter : IBankGateway
{
    private readonly InMemoryBankBackend _backend;

    public BankGatewayAdapter(InMemoryBankBackend backend) => _backend = backend;

    public Task<CardInfoResponse> GetCardInfoAsync(string cardNumber, CancellationToken ct)
        => Task.FromResult(_backend.GetCard(cardNumber));

    public Task<PinVerifyResponse> VerifyPinAsync(string cardNumber, string pin, CancellationToken ct)
        => Task.FromResult(_backend.VerifyPin(cardNumber, pin));

    public Task<BankBalanceResponse> GetBalanceAsync(string accountId, CancellationToken ct)
        => Task.FromResult(_backend.GetBalance(accountId));

    public Task<ReserveFundsResponse> ReserveFundsAsync(string accountId, long amountCents, string transactionId, CancellationToken ct)
        => Task.FromResult(_backend.ReserveFunds(accountId, amountCents, transactionId));

    public Task<FinalizeResponse> FinalizeWithdrawalAsync(string transactionId, CancellationToken ct)
        => Task.FromResult(_backend.FinalizeWithdrawal(transactionId));

    public Task<CancelResponse> CancelReservationAsync(string transactionId, CancellationToken ct)
        => Task.FromResult(_backend.CancelReservation(transactionId));

    public Task<DepositResponse> PostDepositAsync(string accountId, long amountCents, string transactionId, CancellationToken ct)
        => Task.FromResult(_backend.PostDeposit(accountId, amountCents, transactionId));
}