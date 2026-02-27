namespace TaskManagementSystem.Infrastructure.Concurrency;

public sealed class ConcurrencyException : Exception
{
    public ConcurrencyException(string message) : base(message) { }
}
