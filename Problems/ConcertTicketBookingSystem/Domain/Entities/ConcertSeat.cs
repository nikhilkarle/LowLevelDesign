using ConcertTicketBookingSystem.Domain.Enums;
using ConcertTicketBookingSystem.Domain.States;

namespace ConcertTicketBookingSystem.Domain.Entities;

public class ConcertSeat
{
    private ISeatState _state;
    private int        _version;

    public Guid      Id           { get; }
    public Guid      ConcertId    { get; }
    public Guid      VenueSeatId  { get; }
    public decimal   Price        { get; }
    public int       Version      => _version;
    public SeatStatus Status      => _state.Status;

    internal Guid?     ReservedBy    { get; set; }
    internal DateTime? ReservedUntil { get; set; }

    public ConcertSeat(Guid id, Guid concertId, Guid venueSeatId, decimal price)
    {
        Id = id; ConcertId = concertId; VenueSeatId = venueSeatId; Price = price;
        _state = new AvailableState();
    }

    internal void TransitionTo(ISeatState newState) => _state = newState;

    public void Reserve(Guid userId) => _state.Reserve(this, userId);
    public void Book()               => _state.Book(this);
    public void Release()            => _state.Release(this);

    internal bool TryIncrementVersion(int expected) =>
        Interlocked.CompareExchange(ref _version, expected + 1, expected) == expected;
}
