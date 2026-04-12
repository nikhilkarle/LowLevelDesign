using ConcertTicketBookingSystem.Domain.Entities;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Domain.States;

public class BookedState : ISeatState
{
    public SeatStatus Status => SeatStatus.Booked;

    public void Reserve(ConcertSeat seat, Guid userId) =>
        throw new InvalidOperationException("Seat is already booked.");

    public void Book(ConcertSeat seat) =>
        throw new InvalidOperationException("Seat is already booked.");

    public void Release(ConcertSeat seat) =>
        seat.TransitionTo(new AvailableState());
}
