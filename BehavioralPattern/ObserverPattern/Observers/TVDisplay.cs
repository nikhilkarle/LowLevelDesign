using ObserverPattern.Models;
using System;

namespace ObserverPattern.Observers
{
    public class TVDisplay : IObserver
    {
        public void Update(WeatherReading weatherReading)
        {
            Console.WriteLine($"[TV]: Temp: {weatherReading.TemperatureC}C and Humidity: {weatherReading.HumidityPercent}");
        }
    }
}