using AtmSystem.Domain;

namespace AtmSystem.Auth;

public sealed record AuthContext(string CardNumber, string Pin, int FailedPinAttempts);

public interface IAuthStep
{
    Task<AuthResult> HandleAsync(AuthContext ctx, Func<Task<AuthResult>> next, CancellationToken ct);
}