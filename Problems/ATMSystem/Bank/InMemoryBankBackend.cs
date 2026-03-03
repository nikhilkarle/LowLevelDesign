using System.Collections.Concurrent;
using AtmSystem.Domain;

namespace AtmSystem.Bank;

public sealed class InMemoryBankBackend
{
    private sealed class AccountRecord
    {
        public string AccountId { get; }
        public long BalanceCents { get; set; }
        public bool IsActive { get; set; }
        public string Pin { get; set; }
        public int Version { get; set; } = 0;

        public object Sync { get; } = new();

        public AccountRecord(string accountId, long balanceCents, string pin, bool isActive)
        {
            AccountId = accountId;
            BalanceCents = balanceCents;
            Pin = pin;
            IsActive = isActive;
        }
    }

    private readonly ConcurrentDictionary<string, Card> _cards = new();
    private readonly ConcurrentDictionary<string, AccountRecord> _accounts = new();

    private readonly ConcurrentDictionary<string, Reservation> _reservations = new();

    private readonly ConcurrentDictionary<string, string> _completed = new();

    public void UpsertAccount(string accountId, long balanceCents, string pin, bool isActive)
    {
        _accounts.AddOrUpdate(
            accountId,
            _ => new AccountRecord(accountId, balanceCents, pin, isActive),
            (_, existing) =>
            {
                lock (existing.Sync)
                {
                    existing.BalanceCents = balanceCents;
                    existing.Pin = pin;
                    existing.IsActive = isActive;
                    existing.Version++;
                    return existing;
                }
            });
    }

    public void UpsertCard(string cardNumber, string linkedAccountId, bool isActive, DateTime expiresUtc)
    {
        _cards[cardNumber] = new Card(cardNumber, linkedAccountId, isActive, expiresUtc);
    }

    public CardInfoResponse GetCard(string cardNumber)
    {
        if (!_cards.TryGetValue(cardNumber, out var card))
            return new CardInfoResponse(false, "Card not found.", null);

        return new CardInfoResponse(true, null, card);
    }

    public PinVerifyResponse VerifyPin(string cardNumber, string pin)
    {
        if (!_cards.TryGetValue(cardNumber, out var card))
            return new PinVerifyResponse(false, "Card not found.", null);

        if (!_accounts.TryGetValue(card.LinkedAccountId, out var acct))
            return new PinVerifyResponse(false, "Linked account not found.", null);

        lock (acct.Sync)
        {
            if (!acct.IsActive) return new PinVerifyResponse(false, "Account is inactive.", null);
            if (!string.Equals(acct.Pin, pin, StringComparison.Ordinal))
                return new PinVerifyResponse(false, "Invalid PIN.", null);

            return new PinVerifyResponse(true, null, acct.AccountId);
        }
    }

    public BankBalanceResponse GetBalance(string accountId)
    {
        if (!_accounts.TryGetValue(accountId, out var acct))
            return new BankBalanceResponse(false, "Account not found.", 0);

        lock (acct.Sync)
        {
            if (!acct.IsActive) return new BankBalanceResponse(false, "Account inactive.", 0);
            return new BankBalanceResponse(true, null, acct.BalanceCents);
        }
    }

    public ReserveFundsResponse ReserveFunds(string accountId, long amountCents, string transactionId)
    {
        if (_completed.ContainsKey(transactionId))
            return new ReserveFundsResponse(true, null);

        if (_reservations.ContainsKey(transactionId))
            return new ReserveFundsResponse(true, null);

        if (!_accounts.TryGetValue(accountId, out var acct))
            return new ReserveFundsResponse(false, "Account not found.");

        lock (acct.Sync)
        {
            if (!acct.IsActive) return new ReserveFundsResponse(false, "Account inactive.");
            if (amountCents <= 0) return new ReserveFundsResponse(false, "Invalid amount.");
            if (acct.BalanceCents < amountCents) return new ReserveFundsResponse(false, "Insufficient funds.");

            var reservation = new Reservation(transactionId, accountId, amountCents, DateTime.UtcNow);
            if (!_reservations.TryAdd(transactionId, reservation))
                return new ReserveFundsResponse(true, null);

            acct.Version++;
            return new ReserveFundsResponse(true, null);
        }
    }

    public FinalizeResponse FinalizeWithdrawal(string transactionId)
    {
        if (_completed.TryGetValue(transactionId, out var marker) && marker == "WITHDRAW_FINALIZED")
            return new FinalizeResponse(true, null);

        if (!_reservations.TryGetValue(transactionId, out var reservation))
            return new FinalizeResponse(false, "No reservation found to finalize.");

        if (!_accounts.TryGetValue(reservation.AccountId, out var acct))
            return new FinalizeResponse(false, "Account not found.");

        lock (acct.Sync)
        {
            if (acct.BalanceCents < reservation.AmountCents)
                return new FinalizeResponse(false, "Insufficient funds at finalize (unexpected).");

            acct.BalanceCents -= reservation.AmountCents;
            acct.Version++;

            _reservations.TryRemove(transactionId, out _);
            _completed[transactionId] = "WITHDRAW_FINALIZED";

            return new FinalizeResponse(true, null);
        }
    }

    public CancelResponse CancelReservation(string transactionId)
    {
        if (_completed.ContainsKey(transactionId))
            return new CancelResponse(false, "Transaction already completed; cannot cancel.");

        if (_reservations.TryRemove(transactionId, out _))
            return new CancelResponse(true, null);

        return new CancelResponse(true, null);
    }

    public DepositResponse PostDeposit(string accountId, long amountCents, string transactionId)
    {
        if (_completed.TryGetValue(transactionId, out var marker) && marker == "DEPOSIT_POSTED")
            return new DepositResponse(true, null);

        if (!_accounts.TryGetValue(accountId, out var acct))
            return new DepositResponse(false, "Account not found.");

        lock (acct.Sync)
        {
            if (!acct.IsActive) return new DepositResponse(false, "Account inactive.");
            if (amountCents <= 0) return new DepositResponse(false, "Invalid amount.");

            acct.BalanceCents += amountCents;
            acct.Version++;

            _completed[transactionId] = "DEPOSIT_POSTED";
            return new DepositResponse(true, null);
        }
    }
}