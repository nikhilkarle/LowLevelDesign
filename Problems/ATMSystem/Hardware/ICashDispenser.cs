namespace AtmSystem.Hardware;

public interface ICashDispenser
{
    Task<(bool Success, string? Error)> DispenseAsync(CashPlan plan, CancellationToken ct);
}