using PubSubSystem.Models;

namespace PubSubSystem.Interfaces;

public interface ISubscriber
{
    string Id { get; }
    Task OnMessageAsync(Message message, CancellationToken cancellationToken = default);
}