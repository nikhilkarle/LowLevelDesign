using PubSubSystem.Models;

namespace PubSubSystem.Interfaces;

public interface IMessageFactory
{
    Message Create(string topic, string payload);
}