using AtmSystem.Bank;
using AtmSystem.Domain;

namespace AtmSystem.Auth;

public sealed class PinVerifyStep : IAuthStep
{
    private readonly IBankGateway _bank;

    public PinVerifyStep(IBankGateway bank) => _bank = bank;

    public async Task<AuthResult> HandleAsync(AuthContext ctx, Func<Task<AuthResult>> next, CancellationToken ct)
    {
        var verify = await _bank.VerifyPinAsync(ctx.CardNumber, ctx.Pin, ct);
        if (!verify.Success) return AuthResult.Fail(verify.Error ?? "PIN verification failed.");

        return AuthResult.Ok(verify.AccountId!);
    }
}