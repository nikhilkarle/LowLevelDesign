namespace VendingMachine.Events;

public interface IEventPublisher
{
    void Publish(IVendingEvent evt);
    void Subscribe(Action<IVendingEvent> handler);
}
