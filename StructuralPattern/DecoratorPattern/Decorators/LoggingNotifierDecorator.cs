using DecoratorPattern.Notifier;
using System;

namespace DecoratorPattern.Decorators
{
    public class LoggingNotifierDecorator : NotifierDecorator
    {
        public LoggingNotifierDecorator(INotifier notifier) : base(notifier)
        {
        }

        public override bool Send(string to, string message)
        {
            Console.WriteLine("[LOG] About to send message to " + to);

            bool success = GetNotifier.Send(to, message);

            Console.WriteLine("[LOG] Send result = " + success);
            return success;
        }
    }
}