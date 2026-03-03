using AtmSystem.Bank;
using AtmSystem.Domain;

namespace AtmSystem.Auth;

public sealed class CardActiveStep : IAuthStep
{
    private readonly IBankGateway _bank;

    public CardActiveStep(IBankGateway bank) => _bank = bank;

    public async Task<AuthResult> HandleAsync(AuthContext ctx, Func<Task<AuthResult>> next, CancellationToken ct)
    {
        var cardInfo = await _bank.GetCardInfoAsync(ctx.CardNumber, ct);
        if (!cardInfo.Success) return AuthResult.Fail(cardInfo.Error ?? "Card lookup failed.");
        if (cardInfo.Card is null) return AuthResult.Fail("Card not recognized.");
        if (!cardInfo.Card.IsActive) return AuthResult.Fail("Card is blocked.");
        if (cardInfo.Card.IsExpiredUtc(DateTime.UtcNow)) return AuthResult.Fail("Card expired.");

        return await next();
    }
}