using AtmSystem.Bank;

namespace AtmSystem.Auth;

public static class AuthPipelineFactory
{
    public static AuthPipeline CreateDefault(IBankGateway bank)
        => new AuthPipeline(new IAuthStep[]
        {
            new CardPresentStep(),
            new RetryLimitStep(maxAttempts: 3),
            new CardActiveStep(bank),
            new PinVerifyStep(bank)
        });
}