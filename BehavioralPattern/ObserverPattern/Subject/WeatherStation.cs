using ObserverPattern.Models;
using ObserverPattern.Observers;

namespace ObserverPattern.Subject
{
    public class WeatherStation : ISubject
    {
        private readonly List<IObserver> _observers = new();
        private WeatherReading _currentReading = new(0m,0);

        public void Subscribe(IObserver observer)
        {
            if (!_observers.Contains(observer))
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            if (_observers.Contains(observer))
            _observers.Remove(observer);
        }

        public void SetReading(WeatherReading weatherReading)
        {
            _currentReading = weatherReading;
            Notify();
        }

        private void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_currentReading);
            }
        }
    }
}