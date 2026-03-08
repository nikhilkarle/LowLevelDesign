using PubSubSystem.Core;
using PubSubSystem.Dispatchers;
using PubSubSystem.Factories;
using PubSubSystem.Interfaces;

namespace PubSubSystem;

public static class Program
{
    public static async Task Main(string[] args)
    {
        IMessageDispatcher dispatcher = new ParallelMessageDispatcher();
        IMessageFactory messageFactory = new MessageFactory();
        IPubSubBroker broker = new PubSubBroker(dispatcher);

        ISubscriber subscriber1 = new Subscriber("sub-1");
        ISubscriber subscriber2 = new Subscriber("sub-2");
        ISubscriber subscriber3 = new Subscriber("sub-3");

        broker.Subscribe("sports", subscriber1);
        broker.Subscribe("sports", subscriber2);
        broker.Subscribe("tech", subscriber1);
        broker.Subscribe("tech", subscriber3);
        broker.Subscribe("news", subscriber2);

        IPublisher publisher1 = new Publisher("pub-1", broker, messageFactory);
        IPublisher publisher2 = new Publisher("pub-2", broker, messageFactory);

        await publisher1.PublishAsync("sports", "India won the match.");
        await publisher2.PublishAsync("tech", "C# 14 features discussion.");
        await publisher1.PublishAsync("news", "Market closes higher today.");

        Console.WriteLine();
        Console.WriteLine("---- Unsubscribing sub-2 from sports ----");
        broker.Unsubscribe("sports", subscriber2);

        await publisher2.PublishAsync("sports", "Football finals start at 8 PM.");
    }
}