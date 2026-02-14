using DecoratorPattern.Notifier;
using System;

namespace DecoratorPattern.Decorators
{
    public class RetryNotifierDecorator : NotifierDecorator
    {
        private readonly int _maxAttempts;

        public RetryNotifierDecorator(INotifier notifier, int maxAttempts) : base(notifier)
        {
            _maxAttempts = maxAttempts;
        }

        public override bool Send(string to, string message)
        {
            int currAttempts = 1;

            while (currAttempts <= _maxAttempts)
            {
                bool success = GetNotifier.Send(to, message);

                if (success)
                {
                    return true;
                }

                Console.WriteLine("[RETRY] Attempt " + currAttempts + " failed.");

                currAttempts = currAttempts + 1;
            }
            return false;
        }
    }
}