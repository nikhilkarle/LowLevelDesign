using System;

namespace DecoratorPattern.Notifier
{
    public class EmailNotifier : INotifier
    {
        public bool Send(string to, string messsage)
        {
            Console.WriteLine("Email sent to: " + to);
            return true;
        }
    }
}