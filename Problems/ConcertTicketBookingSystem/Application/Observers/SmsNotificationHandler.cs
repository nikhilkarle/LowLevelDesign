using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Application.Observers;

public class SmsNotificationHandler : IBookingEventHandler
{
    private readonly IUserRepository     _userRepo;
    private readonly INotificationSender _sender;

    public SmsNotificationHandler(IUserRepository userRepo, INotificationSender sender)
    {
        _userRepo = userRepo;
        _sender   = sender;
    }

    public void OnBookingConfirmed(Booking booking)
    {
        var user = _userRepo.GetById(booking.UserId);
        if (user is null || user.PreferredChannel != NotificationChannel.SMS) return;

        _sender.Send(user.Phone, $"Booking confirmed! Ref: {booking.Id}. Amount: ${booking.TotalAmount:F2}");
    }

    public void OnBookingCancelled(Booking booking)
    {
        var user = _userRepo.GetById(booking.UserId);
        if (user is null || user.PreferredChannel != NotificationChannel.SMS) return;

        _sender.Send(user.Phone, $"Your booking {booking.Id} has been cancelled.");
    }
}
