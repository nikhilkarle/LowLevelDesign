namespace BridgePattern.Implementations
{
    public class EmailNotification : INotification
    {
        public void SendMessage(string to, string message)
        {
            Console.WriteLine("Email sent to " + to + " with message " + message);
        }
    }
}