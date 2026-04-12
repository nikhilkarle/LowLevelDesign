using ConcertTicketBookingSystem.Domain.Entities;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Domain.States;

public interface ISeatState
{
    SeatStatus Status { get; }
    void Reserve(ConcertSeat seat, Guid userId);
    void Book(ConcertSeat seat);
    void Release(ConcertSeat seat);
}
