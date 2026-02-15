using BridgePattern.Implementations;

namespace BridgePattern.Abstractions
{
    public abstract class NotificationAbstraction
    {
        private readonly INotification _sender;

        protected NotificationAbstraction(INotification sender)
        {
            _sender = sender;
        }

        protected INotification Sender
        {
            get { return _sender; }
        }

        public abstract void Notify(string to, string message);
    }
}