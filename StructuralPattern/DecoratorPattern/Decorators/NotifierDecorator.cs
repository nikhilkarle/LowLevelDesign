using DecoratorPattern.Notifier;

namespace DecoratorPattern.Decorators
{
    public abstract class NotifierDecorator : INotifier
    {
        private readonly INotifier _notifier;

        protected NotifierDecorator(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected INotifier GetNotifier
        {
            get {return _notifier;}
        }

        public virtual bool Send(string to, string message)
        {
            return _notifier.Send(to, message);
        }
    }
}