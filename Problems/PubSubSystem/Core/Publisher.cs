using PubSubSystem.Interfaces;

namespace PubSubSystem.Core;

public sealed class Publisher : IPublisher
{
    private readonly IPubSubBroker _broker;
    private readonly IMessageFactory _messageFactory;

    public string Id { get; }

    public Publisher(string id, IPubSubBroker broker, IMessageFactory messageFactory)
    {
        Id = string.IsNullOrWhiteSpace(id)
            ? throw new ArgumentException("Publisher Id cannot be null or empty.", nameof(id))
            : id;

        _broker = broker ?? throw new ArgumentNullException(nameof(broker));
        _messageFactory = messageFactory ?? throw new ArgumentNullException(nameof(messageFactory));
    }

    public async Task PublishAsync(string topic, string payload, CancellationToken cancellationToken = default)
    {
        var message = _messageFactory.Create(topic, payload);
        await _broker.PublishAsync(message, cancellationToken);
    }
}