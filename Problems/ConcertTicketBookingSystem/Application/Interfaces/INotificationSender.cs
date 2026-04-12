namespace ConcertTicketBookingSystem.Application.Interfaces;

public interface INotificationSender
{
    void Send(string recipient, string message);
}
