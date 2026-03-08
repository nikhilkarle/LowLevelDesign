namespace PubSubSystem.Models;

public sealed class Message
{
    public Guid Id { get; }
    public string Topic { get; }
    public string Payload { get; }
    public DateTime CreatedAtUtc { get; }

    public Message(string topic, string payload)
    {
        if (string.IsNullOrWhiteSpace(topic))
            throw new ArgumentException("Topic cannot be null or empty.", nameof(topic));

        if (payload is null)
            throw new ArgumentNullException(nameof(payload));

        Id = Guid.NewGuid();
        Topic = topic;
        Payload = payload;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return $"MessageId={Id}, Topic={Topic}, Payload={Payload}, CreatedAtUtc={CreatedAtUtc:O}";
    }
}