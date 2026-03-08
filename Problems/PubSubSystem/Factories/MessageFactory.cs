using PubSubSystem.Interfaces;
using PubSubSystem.Models;

namespace PubSubSystem.Factories;

public sealed class MessageFactory : IMessageFactory
{
    public Message Create(string topic, string payload)
    {
        return new Message(topic, payload);
    }
}