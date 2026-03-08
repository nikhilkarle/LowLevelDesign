namespace PubSubSystem.Interfaces;

public interface IPublisher
{
    string Id { get; }
    Task PublishAsync(string topic, string payload, CancellationToken cancellationToken = default);
}