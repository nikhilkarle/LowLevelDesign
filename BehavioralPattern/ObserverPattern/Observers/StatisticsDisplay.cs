using ObserverPattern.Models;
using System;

namespace ObserverPattern.Observers
{
    public class StatisticsDisplay : IObserver
    {
        private decimal? _min;
        private decimal? _max;

        public void Update(WeatherReading weatherReading)
        {
            _min = _min is null ? weatherReading.TemperatureC : Math.Min(_min.Value, weatherReading.TemperatureC);
            _max = _max is null ? weatherReading.TemperatureC : Math.Max(_max.Value, weatherReading.TemperatureC);

            Console.WriteLine($"[Stats] Min: {_min}, Max: {_max}");
        }
    }
}