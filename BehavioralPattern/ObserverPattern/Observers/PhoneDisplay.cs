using ObserverPattern.Models;
using System;

namespace ObserverPattern.Observers
{
    public class PhoneDisplay : IObserver
    {
        public void Update(WeatherReading weatherReading)
        {
            Console.WriteLine($"[Phone]: Temp: {weatherReading.TemperatureC}C and Humidity: {weatherReading.HumidityPercent}");
        }
    }
}