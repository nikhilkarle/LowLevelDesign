using PubSubSystem.Models;

namespace PubSubSystem.Interfaces;

public interface IMessageDispatcher
{
    Task DispatchAsync(
        Message message,
        IReadOnlyCollection<ISubscriber> subscribers,
        CancellationToken cancellationToken = default);
}