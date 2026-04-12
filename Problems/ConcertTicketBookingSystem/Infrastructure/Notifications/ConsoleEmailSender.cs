using ConcertTicketBookingSystem.Application.Interfaces;

namespace ConcertTicketBookingSystem.Infrastructure.Notifications;

public class ConsoleEmailSender : INotificationSender
{
    public void Send(string recipient, string message) =>
        Console.WriteLine($"  [EMAIL → {recipient}] {message}");
}
