namespace OAS.Domain.Models;

public class NotificationMessage
{
    public string Subject { get; }
    public string Body { get; }

    public NotificationMessage(string subject, string body)
    {
        Subject = subject;
        Body = body;
    }
}