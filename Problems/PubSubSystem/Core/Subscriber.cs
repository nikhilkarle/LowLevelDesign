using PubSubSystem.Interfaces;
using PubSubSystem.Models;

namespace PubSubSystem.Core;

public sealed class Subscriber : ISubscriber
{
    public string Id { get; }

    public Subscriber(string id)
    {
        Id = string.IsNullOrWhiteSpace(id)
            ? throw new ArgumentException("Subscriber Id cannot be null or empty.", nameof(id))
            : id;
    }

    public Task OnMessageAsync(Message message, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Subscriber [{Id}] received -> {message}");
        return Task.CompletedTask;
    }
}