using AtmSystem.Bank;
using AtmSystem.Domain;
using AtmSystem.Hardware;

namespace AtmSystem.Atm;

public interface ITransaction
{
    Task<TransactionResult> ExecuteAsync(AtmSession session, CancellationToken ct);
}

public sealed class TransactionFactory
{
    private readonly IBankGateway _bank;
    private readonly ICashDispenser _dispenser;
    private readonly IDepositAcceptor _depositAcceptor;
    private readonly IAtmCashStore _cashStore;
    private readonly SemaphoreSlim _dispenseLock;

    public TransactionFactory(
        IBankGateway bank,
        ICashDispenser dispenser,
        IDepositAcceptor depositAcceptor,
        IAtmCashStore cashStore,
        SemaphoreSlim dispenseLock)
    {
        _bank = bank;
        _dispenser = dispenser;
        _depositAcceptor = depositAcceptor;
        _cashStore = cashStore;
        _dispenseLock = dispenseLock;
    }

    public ITransaction Create(TransactionType type) => type switch
    {
        TransactionType.BalanceInquiry => new BalanceInquiryTransaction(_bank),
        TransactionType.Withdraw => new WithdrawTransaction(_bank, _dispenser, _cashStore, _dispenseLock),
        TransactionType.Deposit => new DepositTransaction(_bank, _depositAcceptor),
        _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported transaction type")
    };

    private sealed class BalanceInquiryTransaction : ITransaction
    {
        private readonly IBankGateway _bank;

        public BalanceInquiryTransaction(IBankGateway bank) => _bank = bank;

        public async Task<TransactionResult> ExecuteAsync(AtmSession session, CancellationToken ct)
        {
            if (session.AccountId is null) return TransactionResult.Fail("Not authenticated.");

            var resp = await _bank.GetBalanceAsync(session.AccountId, ct);
            if (!resp.Success) return TransactionResult.Fail(resp.Error ?? "Balance inquiry failed.");

            var receipt = $"BALANCE INQUIRY\nAccount: {session.AccountId}\nBalance: ${(resp.BalanceCents / 100.0):0.00}\n";
            return TransactionResult.Ok(receipt);
        }
    }

    private sealed class WithdrawTransaction : ITransaction
    {
        private readonly IBankGateway _bank;
        private readonly ICashDispenser _dispenser;
        private readonly IAtmCashStore _cashStore;
        private readonly SemaphoreSlim _dispenseLock;

        public WithdrawTransaction(IBankGateway bank, ICashDispenser dispenser, IAtmCashStore cashStore, SemaphoreSlim dispenseLock)
        {
            _bank = bank;
            _dispenser = dispenser;
            _cashStore = cashStore;
            _dispenseLock = dispenseLock;
        }

        public async Task<TransactionResult> ExecuteAsync(AtmSession session, CancellationToken ct)
        {
            if (session.AccountId is null) return TransactionResult.Fail("Not authenticated.");
            if (session.RequestedAmountCents <= 0) return TransactionResult.Fail("Invalid withdrawal amount.");

            var amountCents = session.RequestedAmountCents;

            if (amountCents % 100 != 0) return TransactionResult.Fail("ATM dispenses whole dollars only.");

            var amountDollars = (int)(amountCents / 100);

            var plan = _cashStore.TryAllocate(amountDollars);
            if (plan is null) return TransactionResult.Fail("ATM cannot dispense that amount with available denominations.");

            var txnId = session.NewTransactionId();

            var reserve = await _bank.ReserveFundsAsync(session.AccountId, amountCents, txnId, ct);
            if (!reserve.Success) return TransactionResult.Fail(reserve.Error ?? "Could not reserve funds.");

            await _dispenseLock.WaitAsync(ct);
            try
            {
                var disp = await _dispenser.DispenseAsync(plan, ct);
                if (!disp.Success)
                {
                    await _bank.CancelReservationAsync(txnId, ct);
                    return TransactionResult.Fail("Dispense failed. Reservation was cancelled.");
                }

                _cashStore.Commit(plan);
            }
            finally
            {
                _dispenseLock.Release();
            }

            var fin = await _bank.FinalizeWithdrawalAsync(txnId, ct);
            if (!fin.Success)
            {
                return TransactionResult.Fail("Cash dispensed, but finalization needs reconciliation.");
            }

            var receipt =
                $"WITHDRAWAL\nAccount: {session.AccountId}\nAmount: ${(amountCents / 100.0):0.00}\nTxnId: {txnId}\n";
            return TransactionResult.Ok(receipt);
        }
    }

    private sealed class DepositTransaction : ITransaction
    {
        private readonly IBankGateway _bank;
        private readonly IDepositAcceptor _depositAcceptor;

        public DepositTransaction(IBankGateway bank, IDepositAcceptor depositAcceptor)
        {
            _bank = bank;
            _depositAcceptor = depositAcceptor;
        }

        public async Task<TransactionResult> ExecuteAsync(AtmSession session, CancellationToken ct)
        {
            if (session.AccountId is null) return TransactionResult.Fail("Not authenticated.");
            if (session.RequestedAmountCents <= 0) return TransactionResult.Fail("Invalid deposit amount.");

            var txnId = session.NewTransactionId();

            var accepted = await _depositAcceptor.AcceptDepositAsync(session.RequestedAmountCents, ct);
            if (!accepted.Success) return TransactionResult.Fail("Deposit acceptor error.");

            var dep = await _bank.PostDepositAsync(session.AccountId, session.RequestedAmountCents, txnId, ct);
            if (!dep.Success) return TransactionResult.Fail(dep.Error ?? "Deposit posting failed.");

            var receipt =
                $"DEPOSIT\nAccount: {session.AccountId}\nAmount: ${(session.RequestedAmountCents / 100.0):0.00}\nTxnId: {txnId}\n";
            return TransactionResult.Ok(receipt);
        }
    }
}