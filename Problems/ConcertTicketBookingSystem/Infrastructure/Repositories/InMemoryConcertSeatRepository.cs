using System.Collections.Concurrent;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;
using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Infrastructure.Repositories;

public class InMemoryConcertSeatRepository : IConcertSeatRepository
{
    private readonly ConcurrentDictionary<Guid, ConcertSeat> _seats = new();

    private readonly object _lock = new();

    public ConcertSeat? GetById(Guid id) => _seats.GetValueOrDefault(id);

    public IEnumerable<ConcertSeat> GetByConcert(Guid concertId) =>
        _seats.Values.Where(s => s.ConcertId == concertId);

    public IEnumerable<ConcertSeat> GetAvailableByConcert(Guid concertId) =>
        _seats.Values.Where(s => s.ConcertId == concertId && s.Status == SeatStatus.Available);

    public bool TryReserve(Guid seatId, Guid userId, int expectedVersion)
    {
        lock (_lock)
        {
            if (!_seats.TryGetValue(seatId, out var seat)) return false;
            if (seat.Status != SeatStatus.Available) return false;

            if (!seat.TryIncrementVersion(expectedVersion)) return false;

            seat.Reserve(userId);
            return true;
        }
    }

    public void ReleaseExpired()
    {
        lock (_lock)
        {
            foreach (var seat in _seats.Values.Where(s =>
                         s.Status == SeatStatus.Reserved &&
                         s.ReservedUntil.HasValue &&
                         s.ReservedUntil.Value < DateTime.UtcNow))
            {
                seat.Release();
            }
        }
    }

    public void Add(ConcertSeat seat) => _seats[seat.Id] = seat;
}
