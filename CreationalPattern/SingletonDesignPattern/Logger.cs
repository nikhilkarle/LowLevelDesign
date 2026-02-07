using System;

namespace SingletonDesignPattern
{
    public sealed class Logger
    {
        private static readonly Lazy<Logger> _instance = new Lazy<Logger>(() => new Logger());

        public static Logger Instance => _instance.Value;

        private Logger() {}

        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }
}