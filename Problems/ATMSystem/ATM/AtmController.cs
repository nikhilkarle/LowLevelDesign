using AtmSystem.Auth;
using AtmSystem.Bank;
using AtmSystem.Domain;
using AtmSystem.Hardware;

namespace AtmSystem.Atm;

public sealed class AtmController
{
    private readonly ICardReader _cardReader;
    private readonly IPinPad _pinPad;
    private readonly ICashDispenser _dispenser;
    private readonly IDepositAcceptor _depositAcceptor;
    private readonly IReceiptPrinter _printer;
    private readonly IAtmCashStore _cashStore;
    private readonly IBankGateway _bankGateway;
    private readonly AuthPipeline _authPipeline;

    private readonly SemaphoreSlim _dispenseLock = new(1, 1);

    private AtmState _state = AtmState.Idle;
    private readonly AtmSession _session = new();

    public AtmController(
        ICardReader cardReader,
        IPinPad pinPad,
        ICashDispenser dispenser,
        IDepositAcceptor depositAcceptor,
        IReceiptPrinter printer,
        IAtmCashStore cashStore,
        IBankGateway bankGateway,
        AuthPipeline authPipeline)
    {
        _cardReader = cardReader;
        _pinPad = pinPad;
        _dispenser = dispenser;
        _depositAcceptor = depositAcceptor;
        _printer = printer;
        _cashStore = cashStore;
        _bankGateway = bankGateway;
        _authPipeline = authPipeline;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("=== ATM System (LLD) ===");
        Console.WriteLine("Type 'exit' at card prompt to quit.\n");

        while (true)
        {
            _session.Reset();
            _state = AtmState.Idle;

            // 1) Insert card
            _state = AtmState.Idle;
            var card = await _cardReader.ReadCardAsync();
            if (card is null) continue;
            if (card.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;

            _session.SetCard(card);
            _state = AtmState.CardInserted;

            // 2) PIN entry + auth
            _state = AtmState.PinEntry;
            var authenticated = await AuthenticateAsync();
            if (!authenticated)
            {
                await EjectAsync("Authentication failed.");
                continue;
            }

            _state = AtmState.Authenticated;

            // 3) Transaction loop (single transaction per session for simplicity)
            _state = AtmState.TransactionSelection;
            var selected = PromptTransactionType();
            if (selected is null)
            {
                await EjectAsync("Cancelled.");
                continue;
            }

            _session.SelectedTransaction = selected.Value;

            // 4) Collect amount if needed
            if (selected.Value is TransactionType.Withdraw or TransactionType.Deposit)
            {
                var amountCents = PromptAmountCents(selected.Value);
                if (amountCents <= 0)
                {
                    await EjectAsync("Invalid amount.");
                    continue;
                }

                _session.RequestedAmountCents = amountCents;
            }

            // 5) Execute transaction
            _state = AtmState.Processing;

            var factory = new TransactionFactory(_bankGateway, _dispenser, _depositAcceptor, _cashStore, _dispenseLock);
            var txn = factory.Create(selected.Value);

            var result = await txn.ExecuteAsync(_session, CancellationToken.None);
            if (!result.Success)
            {
                await _printer.PrintAsync($"ERROR: {result.Error}");
                await EjectAsync("Done.");
                continue;
            }

            if (!string.IsNullOrWhiteSpace(result.Receipt))
                await _printer.PrintAsync(result.Receipt!);

            await EjectAsync("Thank you.");
        }
    }

    private async Task<bool> AuthenticateAsync()
    {
        for (int attempt = 1; attempt <= 3; attempt++)
        {
            var pin = await _pinPad.ReadPinAsync();
            if (pin is null) return false;

            var ctx = new AuthContext(
                CardNumber: _session.CardNumber!,
                Pin: pin,
                FailedPinAttempts: _session.FailedPinAttempts
            );

            var auth = await _authPipeline.ExecuteAsync(ctx, CancellationToken.None);
            if (auth.Success && auth.AccountId is not null)
            {
                _session.SetAccount(auth.AccountId);
                _session.ResetPinFailures();
                return true;
            }

            _session.IncrementPinFailures();
            Console.WriteLine($"Auth failed: {auth.Error}");

            if (attempt == 3) return false;
        }

        return false;
    }

    private static TransactionType? PromptTransactionType()
    {
        Console.WriteLine("\nSelect transaction:");
        Console.WriteLine("  1) Balance Inquiry");
        Console.WriteLine("  2) Withdraw");
        Console.WriteLine("  3) Deposit");
        Console.Write("Choice: ");

        var input = Console.ReadLine()?.Trim();
        return input switch
        {
            "1" => TransactionType.BalanceInquiry,
            "2" => TransactionType.Withdraw,
            "3" => TransactionType.Deposit,
            _ => null
        };
    }

    private static long PromptAmountCents(TransactionType type)
    {
        Console.Write(type == TransactionType.Withdraw
            ? "Enter withdrawal amount in dollars (e.g., 40): "
            : "Enter deposit amount in dollars (e.g., 50): ");

        var s = Console.ReadLine()?.Trim();
        if (!int.TryParse(s, out var dollars)) return -1;
        if (dollars <= 0) return -1;

        return dollars * 100L;
    }

    private async Task EjectAsync(string message)
    {
        _state = AtmState.Ejecting;
        Console.WriteLine($"\n{message}");
        Console.WriteLine("Ejecting card...\n");
        await Task.Delay(250);
        _state = AtmState.Idle;
    }
}