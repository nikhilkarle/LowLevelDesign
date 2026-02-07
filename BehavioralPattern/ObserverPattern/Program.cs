using ObserverPattern.Models;
using ObserverPattern.Observers;
using ObserverPattern.Subject;

class Program
{
    public  static void Main()
    {
        var station = new WeatherStation();

        var phone = new PhoneDisplay();
        var tv = new TVDisplay();
        var stats = new StatisticsDisplay();

        station.Subscribe(phone);
        station.Subscribe(tv);
        station.Subscribe(stats);

        station.SetReading(new WeatherReading(10m,5));
        station.SetReading(new WeatherReading(15m,6));

        station.Unsubscribe(phone);

        station.SetReading(new WeatherReading(7m,4));

    }
}