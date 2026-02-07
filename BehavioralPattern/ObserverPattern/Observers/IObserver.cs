using ObserverPattern.Models;

namespace ObserverPattern.Observers
{
    public interface IObserver
    {
        void Update(WeatherReading weatherReading);
    }
}