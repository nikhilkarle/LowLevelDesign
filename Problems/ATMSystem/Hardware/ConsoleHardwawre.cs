namespace AtmSystem.Hardware;

public sealed class ConsoleHardware
{
    public ICardReader CardReader { get; }
    public IPinPad PinPad { get; }
    public ICashDispenser CashDispenser { get; }
    public IDepositAcceptor DepositAcceptor { get; }
    public IReceiptPrinter ReceiptPrinter { get; }

    public ConsoleHardware(IAtmCashStore cashStore)
    {
        CardReader = new ConsoleCardReader();
        PinPad = new ConsolePinPad();
        ReceiptPrinter = new ConsoleReceiptPrinter();
        CashDispenser = new ConsoleCashDispenser(cashStore);
        DepositAcceptor = new ConsoleDepositAcceptor();
    }

    private sealed class ConsoleCardReader : ICardReader
    {
        public Task<string?> ReadCardAsync()
        {
            Console.Write("Insert card (enter card number, or 'exit'): ");
            var s = Console.ReadLine()?.Trim();
            return Task.FromResult(string.IsNullOrWhiteSpace(s) ? null : s);
        }
    }

    private sealed class ConsolePinPad : IPinPad
    {
        public Task<string?> ReadPinAsync()
        {
            Console.Write("Enter PIN: ");
            var s = Console.ReadLine()?.Trim();
            return Task.FromResult(string.IsNullOrWhiteSpace(s) ? null : s);
        }
    }

    private sealed class ConsoleCashDispenser : ICashDispenser
    {
        private readonly IAtmCashStore _store;

        public ConsoleCashDispenser(IAtmCashStore store) => _store = store;

        public Task<(bool Success, string? Error)> DispenseAsync(CashPlan plan, CancellationToken ct)
        {
            Console.WriteLine("\nDispensing cash...");
            Console.WriteLine($"Bills: {string.Join(", ", plan.Bills.Select(kv => $"${kv.Key}x{kv.Value}"))}");
            Console.WriteLine($"ATM cash remaining (after commit): (commit happens after dispense)\n");

            return Task.FromResult((true, (string?)null));
        }
    }

    private sealed class ConsoleDepositAcceptor : IDepositAcceptor
    {
        public Task<(bool Success, string? Error)> AcceptDepositAsync(long amountCents, CancellationToken ct)
        {
            Console.WriteLine("\nInsert deposit (simulated). Press Enter to confirm inserted.");
            Console.ReadLine();
            return Task.FromResult((true, (string?)null));
        }
    }

    private sealed class ConsoleReceiptPrinter : IReceiptPrinter
    {
        public Task PrintAsync(string text)
        {
            Console.WriteLine("\n--- RECEIPT ---");
            Console.WriteLine(text);
            Console.WriteLine("--------------\n");
            return Task.CompletedTask;
        }
    }
}