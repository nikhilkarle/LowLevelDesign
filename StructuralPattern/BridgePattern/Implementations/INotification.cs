namespace BridgePattern.Implementations
{
    public interface INotification
    {
        void SendMessage(string to, string message);
    }
}