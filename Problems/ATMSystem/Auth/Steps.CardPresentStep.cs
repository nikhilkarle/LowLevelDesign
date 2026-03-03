using AtmSystem.Domain;

namespace AtmSystem.Auth;

public sealed class CardPresentStep : IAuthStep
{
    public Task<AuthResult> HandleAsync(AuthContext ctx, Func<Task<AuthResult>> next, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(ctx.CardNumber))
            return Task.FromResult(AuthResult.Fail("No card present."));
        return next();
    }
}