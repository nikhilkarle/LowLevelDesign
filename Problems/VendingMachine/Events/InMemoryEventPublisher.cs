namespace VendingMachine.Events;

public sealed class InMemoryEventPublisher : IEventPublisher
{
    private readonly List<Action<IVendingEvent>> _subs = new();

    public void Publish(IVendingEvent evt)
    {
        foreach (var s in _subs.ToArray())
            s(evt);
    }

    public void Subscribe(Action<IVendingEvent> handler) => _subs.Add(handler);
}
