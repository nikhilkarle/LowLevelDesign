using PubSubSystem.Interfaces;
using PubSubSystem.Models;

namespace PubSubSystem.Dispatchers;

public sealed class ParallelMessageDispatcher : IMessageDispatcher
{
    public async Task DispatchAsync(
        Message message,
        IReadOnlyCollection<ISubscriber> subscribers,
        CancellationToken cancellationToken = default)
    {
        var tasks = subscribers.Select(subscriber =>
            subscriber.OnMessageAsync(message, cancellationToken));

        await Task.WhenAll(tasks);
    }
}