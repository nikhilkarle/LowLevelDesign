namespace StackOverflow.Reputation;

public interface IUserActivityObserver
{
    void OnActivity(UserActivityEvent evt);
}
