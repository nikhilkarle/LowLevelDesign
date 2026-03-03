namespace AtmSystem.Hardware;

public interface IDepositAcceptor
{
    Task<(bool Success, string? Error)> AcceptDepositAsync(long amountCents, CancellationToken ct);
}