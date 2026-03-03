using AtmSystem.Auth;
using AtmSystem.Bank;
using AtmSystem.Domain;

public sealed class AuthPipeline
{
    private readonly IReadOnlyList<IAuthStep> _steps;
    public AuthPipeline(IReadOnlyList<IAuthStep> steps) => _steps = steps;

    public Task<AuthResult> ExecuteAsync(AuthContext ctx, CancellationToken ct)
    {
        var index = 0;

        Task<AuthResult> Next()
        {
            if (index >= _steps.Count)
                return Task.FromResult(AuthResult.Fail("No authentication handler accepted the request."));

            var step = _steps[index++];
            return step.HandleAsync(ctx, Next, ct);
        }

        return Next();
    }
}