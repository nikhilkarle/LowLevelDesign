using ConcertTicketBookingSystem.Domain.Entities;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Domain.States;

public class AvailableState : ISeatState
{
    public SeatStatus Status => SeatStatus.Available;

    public void Reserve(ConcertSeat seat, Guid userId)
    {
        seat.ReservedBy    = userId;
        seat.ReservedUntil = DateTime.UtcNow.AddMinutes(10);
        seat.TransitionTo(new ReservedState());
    }

    public void Book(ConcertSeat seat) =>
        throw new InvalidOperationException("Seat must be reserved before it can be booked.");

    public void Release(ConcertSeat seat) =>
        throw new InvalidOperationException("Seat is already available.");
}
