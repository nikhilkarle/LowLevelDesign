using ConcertTicketBookingSystem.Application.Interfaces;

namespace ConcertTicketBookingSystem.Infrastructure.Notifications;

public class ConsoleSMSSender : INotificationSender
{
    public void Send(string recipient, string message) =>
        Console.WriteLine($"  [SMS → {recipient}] {message}");
}
