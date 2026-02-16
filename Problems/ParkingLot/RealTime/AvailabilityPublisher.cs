using System.Security.Principal;

namespace ParkingLot.RealTime;

public sealed class AvailabilityPublisher
{
    private readonly object _guard = new();
    private readonly HashSet<IAvailabilityObserver> _observers = new();

    public void Subscribe(IAvailabilityObserver obs)
    {
        lock (_guard) _observers.Add(obs);
    }

    public void Publish(AvailabilitySnapshot snapshot)
    {
        IAvailabilityObserver[] targets;
        lock (_guard) targets = _observers.ToArray();

        foreach (var t in targets) t.OnAvailabilityChanged(snapshot);

    }
}