namespace StackOverflow.Reputation;

public interface IUserActivitySubject
{
    void Subscribe(IUserActivityObserver observer);
    void Unsubscribe(IUserActivityObserver observer);
    void Publish(UserActivityEvent evt);
}
