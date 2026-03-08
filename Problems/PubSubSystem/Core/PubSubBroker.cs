using System.Collections.Concurrent;
using PubSubSystem.Interfaces;
using PubSubSystem.Models;

namespace PubSubSystem.Core;

public sealed class PubSubBroker : IPubSubBroker
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, ISubscriber>> _subscriptions;
    private readonly IMessageDispatcher _dispatcher;

    public PubSubBroker(IMessageDispatcher dispatcher)
    {
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        _subscriptions = new ConcurrentDictionary<string, ConcurrentDictionary<string, ISubscriber>>();
    }

    public void Subscribe(string topic, ISubscriber subscriber)
    {
        if (string.IsNullOrWhiteSpace(topic))
            throw new ArgumentException("Topic cannot be null or empty.", nameof(topic));

        if (subscriber is null)
            throw new ArgumentNullException(nameof(subscriber));

        var subscribersForTopic = _subscriptions.GetOrAdd(
            topic,
            _ => new ConcurrentDictionary<string, ISubscriber>());

        subscribersForTopic[subscriber.Id] = subscriber;
    }

    public void Unsubscribe(string topic, ISubscriber subscriber)
    {
        if (string.IsNullOrWhiteSpace(topic))
            throw new ArgumentException("Topic cannot be null or empty.", nameof(topic));

        if (subscriber is null)
            throw new ArgumentNullException(nameof(subscriber));

        if (_subscriptions.TryGetValue(topic, out var subscribersForTopic))
        {
            subscribersForTopic.TryRemove(subscriber.Id, out _);

            if (subscribersForTopic.IsEmpty)
            {
                _subscriptions.TryRemove(topic, out _);
            }
        }
    }

    public async Task PublishAsync(Message message, CancellationToken cancellationToken = default)
    {
        if (message is null)
            throw new ArgumentNullException(nameof(message));

        if (!_subscriptions.TryGetValue(message.Topic, out var subscribersForTopic) ||
            subscribersForTopic.IsEmpty)
        {
            return;
        }

        var subscriberSnapshot = subscribersForTopic.Values.ToList().AsReadOnly();

        await _dispatcher.DispatchAsync(message, subscriberSnapshot, cancellationToken);
    }
}