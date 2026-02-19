namespace StackOverflow.Reputation;

public sealed class UserActivitySubject : IUserActivitySubject
{
    private readonly List<IUserActivityObserver> _observers = new();

    public void Subscribe(IUserActivityObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IUserActivityObserver observer) => _observers.Remove(observer);

    public void Publish(UserActivityEvent evt)
    {
        foreach (var o in _observers)
            o.OnActivity(evt);
    }
}
