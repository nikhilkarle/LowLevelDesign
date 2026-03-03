using AtmSystem.Atm;
using AtmSystem.Auth;
using AtmSystem.Bank;
using AtmSystem.Hardware;

namespace AtmSystem;

public static class Program
{
    public static async Task Main()
    {
        var cashStore = new InMemoryAtmCashStore(new Dictionary<int, int>
        {
            [20] = 50,
            [10] = 50,
            [5]  = 50
        });

        var hardware = new ConsoleHardware(cashStore);
        var backend  = new InMemoryBankBackend();
        Seed(backend);

        IBankGateway bankGateway = new BankGatewayAdapter(backend);

        var authPipeline = AuthPipelineFactory.CreateDefault(bankGateway);

        var controller = new AtmController(
            cardReader: hardware.CardReader,
            pinPad: hardware.PinPad,
            dispenser: hardware.CashDispenser,
            depositAcceptor: hardware.DepositAcceptor,
            printer: hardware.ReceiptPrinter,
            cashStore: cashStore,
            bankGateway: bankGateway,
            authPipeline: authPipeline
        );

        await controller.RunAsync();
    }

    private static void Seed(InMemoryBankBackend backend)
    {
        backend.UpsertAccount(
            accountId: "A-1001",
            balanceCents: 250_00,           
            pin: "1234",
            isActive: true
        );

        backend.UpsertCard(
            cardNumber: "4111111111111111",
            linkedAccountId: "A-1001",
            isActive: true,
            expiresUtc: DateTime.UtcNow.AddYears(2)
        );

        backend.UpsertAccount(
            accountId: "A-2002",
            balanceCents: 1000_00,          
            pin: "4321",
            isActive: true
        );

        backend.UpsertCard(
            cardNumber: "5555555555554444",
            linkedAccountId: "A-2002",
            isActive: true,
            expiresUtc: DateTime.UtcNow.AddYears(2)
        );
    }
}