using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Observers;

public class EmailNotificationHandler : IBookingEventHandler
{
    private readonly IUserRepository     _userRepo;
    private readonly INotificationSender _sender;

    public EmailNotificationHandler(IUserRepository userRepo, INotificationSender sender)
    {
        _userRepo = userRepo;
        _sender   = sender;
    }

    public void OnBookingConfirmed(Booking booking)
    {
        var user = _userRepo.GetById(booking.UserId);
        if (user is null) return;

        _sender.Send(user.Email,
            $"Booking confirmed! Ref: {booking.Id} | Total: ${booking.TotalAmount:F2} | " +
            $"Concert: {booking.ConcertId}");
    }

    public void OnBookingCancelled(Booking booking)
    {
        var user = _userRepo.GetById(booking.UserId);
        if (user is null) return;

        _sender.Send(user.Email, $"Your booking {booking.Id} has been cancelled.");
    }
}
