namespace BridgePattern.Implementations
{
    public class SMSNotification : INotification
    {
        public void SendMessage(string to, string message)
        {
            Console.WriteLine("SMS sent to " + to + " with message " + message);
        }
    }
}