using DecoratorPattern.Notifier;
using System;
using DecoratorPattern.Decorators;

namespace DecoratorPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            INotifier emailNotifier = new EmailNotifier();
            INotifier logger = new LoggingNotifierDecorator(emailNotifier);
            INotifier retry = new RetryNotifierDecorator(logger, 3);

            bool ok = retry.Send("test@example.com", "Hello from Decorator!");
            Console.WriteLine("Final result: " + ok);
        }
    }
}