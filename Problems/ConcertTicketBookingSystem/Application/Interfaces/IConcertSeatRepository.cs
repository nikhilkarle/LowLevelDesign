using ConcertTicketBookingSystem.Domain.Entities;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Application.Interfaces;

public interface IConcertSeatRepository
{
    ConcertSeat? GetById(Guid id);
    IEnumerable<ConcertSeat> GetByConcert(Guid concertId);
    IEnumerable<ConcertSeat> GetAvailableByConcert(Guid concertId);

    bool TryReserve(Guid seatId, Guid userId, int expectedVersion);

    void ReleaseExpired();

    void Add(ConcertSeat seat);
}
