using PubSubSystem.Models;

namespace PubSubSystem.Interfaces;

public interface IPubSubBroker
{
    void Subscribe(string topic, ISubscriber subscriber);
    void Unsubscribe(string topic, ISubscriber subscriber);
    Task PublishAsync(Message message, CancellationToken cancellationToken = default);
}