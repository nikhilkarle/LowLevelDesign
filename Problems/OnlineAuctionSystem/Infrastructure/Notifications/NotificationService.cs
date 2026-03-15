using OAS.Application.Interfaces;
using OAS.Domain.Models;

namespace OAS.Infrastructure.Notifications;

public class ConsoleNotificationService : INotificationService
{
    public void NotifyUser(Guid userId, NotificationMessage message)
    {
        Console.WriteLine($"[Notify:{userId}] {message.Subject} - {message.Body}");
    }

    public void NotifyUsers(IEnumerable<Guid> userIds, NotificationMessage message)
    {
        foreach (var userId in userIds.Distinct())
        {
            NotifyUser(userId, message);
        }
    }
}