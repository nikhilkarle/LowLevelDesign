using ObserverPattern.Observers;

namespace ObserverPattern.Subject
{
    public interface ISubject
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}