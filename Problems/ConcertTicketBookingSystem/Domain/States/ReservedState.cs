using ConcertTicketBookingSystem.Domain.Entities;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Domain.States;

public class ReservedState : ISeatState
{
    public SeatStatus Status => SeatStatus.Reserved;

    public void Reserve(ConcertSeat seat, Guid userId) =>
        throw new InvalidOperationException("Seat is already reserved.");

    public void Book(ConcertSeat seat)
    {
        seat.ReservedBy    = null;
        seat.ReservedUntil = null;
        seat.TransitionTo(new BookedState());
    }

    public void Release(ConcertSeat seat)
    {
        seat.ReservedBy    = null;
        seat.ReservedUntil = null;
        seat.TransitionTo(new AvailableState());
    }
}
