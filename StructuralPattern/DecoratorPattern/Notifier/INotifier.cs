namespace DecoratorPattern.Notifier
{
    public interface INotifier
    {
        bool Send(string to, string message);
    }
}