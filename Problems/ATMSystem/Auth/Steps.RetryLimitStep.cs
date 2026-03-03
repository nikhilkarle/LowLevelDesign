using AtmSystem.Domain;

namespace AtmSystem.Auth;

public sealed class RetryLimitStep : IAuthStep
{
    private readonly int _maxAttempts;

    public RetryLimitStep(int maxAttempts = 3) => _maxAttempts = maxAttempts;

    public Task<AuthResult> HandleAsync(AuthContext ctx, Func<Task<AuthResult>> next, CancellationToken ct)
    {
        if (ctx.FailedPinAttempts >= _maxAttempts)
            return Task.FromResult(AuthResult.Fail("Too many failed PIN attempts. Card retained."));
        return next();
    }
}