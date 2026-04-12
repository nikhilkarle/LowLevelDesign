using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Application.Factories;

public class NotificationFactory
{
    private readonly INotificationSender _emailSender;
    private readonly INotificationSender _smsSender;

    public NotificationFactory(INotificationSender emailSender, INotificationSender smsSender)
    {
        _emailSender = emailSender;
        _smsSender   = smsSender;
    }

    public INotificationSender Create(NotificationChannel channel) => channel switch
    {
        NotificationChannel.SMS => _smsSender,
        _                       => _emailSender
    };
}
