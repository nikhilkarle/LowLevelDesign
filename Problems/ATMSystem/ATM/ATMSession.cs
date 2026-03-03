using AtmSystem.Domain;

namespace AtmSystem.Atm;

public sealed class AtmSession
{
    public string? CardNumber { get; private set; }
    public string? AccountId { get; private set; }
    public int FailedPinAttempts { get; private set; }

    public TransactionType? SelectedTransaction { get; set; }
    public long RequestedAmountCents { get; set; }

    public void SetCard(string cardNumber) => CardNumber = cardNumber;
    public void SetAccount(string accountId) => AccountId = accountId;

    public void IncrementPinFailures() => FailedPinAttempts++;
    public void ResetPinFailures() => FailedPinAttempts = 0;

    public string NewTransactionId() => Guid.NewGuid().ToString("N");

    public void Reset()
    {
        CardNumber = null;
        AccountId = null;
        FailedPinAttempts = 0;
        SelectedTransaction = null;
        RequestedAmountCents = 0;
    }
}