using BridgePattern.Implementations;

namespace BridgePattern.Abstractions
{
    public class UrgentNotification : NotificationAbstraction
    {
        public UrgentNotification(INotification sender) : base(sender)
        {
        }

        public override void Notify(string to, string message)
        {
            string finalMessage = "Urgent: " + message;
            Sender.SendMessage(to, finalMessage);
        }
    }
}