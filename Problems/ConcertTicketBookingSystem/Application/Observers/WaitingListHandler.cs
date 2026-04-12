using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Observers;

public class WaitingListHandler : IBookingEventHandler
{
    private readonly IWaitingListRepository _waitingListRepo;
    private readonly IUserRepository        _userRepo;
    private readonly INotificationSender    _sender;

    public WaitingListHandler(IWaitingListRepository waitingListRepo,
                              IUserRepository userRepo,
                              INotificationSender sender)
    {
        _waitingListRepo = waitingListRepo;
        _userRepo        = userRepo;
        _sender          = sender;
    }

    public void OnBookingConfirmed(Booking booking) { }

    public void OnBookingCancelled(Booking booking)
    {
        var next = _waitingListRepo.Dequeue(booking.ConcertId);
        if (next is null) return;

        var user = _userRepo.GetById(next.UserId);
        if (user is null) return;

        _sender.Send(user.Email,
            $"Good news! Seats are now available for concert {booking.ConcertId}. " +
            $"You have 15 minutes to complete your booking (Waiting list ref: {next.Id}).");
    }
}
