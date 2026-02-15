using BridgePattern.Implementations;

namespace BridgePattern.Abstractions
{
    public class NormalNotification : NotificationAbstraction
    {
        public NormalNotification(INotification sender) : base(sender)
        {
        }

        public override void Notify(string to, string message)
        {
            string finalMessage = "Normal: " + message;
            Sender.SendMessage(to, finalMessage);
        }
    }
}