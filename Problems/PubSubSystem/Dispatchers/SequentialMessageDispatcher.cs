using PubSubSystem.Interfaces;
using PubSubSystem.Models;

namespace PubSubSystem.Dispatchers;

public sealed class SequentialMessageDispatcher : IMessageDispatcher
{
    public async Task DispatchAsync(
        Message message,
        IReadOnlyCollection<ISubscriber> subscribers,
        CancellationToken cancellationToken = default)
    {
        foreach (var subscriber in subscribers)
        {
            await subscriber.OnMessageAsync(message, cancellationToken);
        }
    }
}