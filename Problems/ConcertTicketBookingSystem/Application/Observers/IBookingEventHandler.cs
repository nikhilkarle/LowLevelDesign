using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Observers;

public interface IBookingEventHandler
{
    void OnBookingConfirmed(Booking booking);
    void OnBookingCancelled(Booking booking);
}
