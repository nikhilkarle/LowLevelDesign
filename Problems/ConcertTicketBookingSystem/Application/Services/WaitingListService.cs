using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Services;

public class WaitingListService
{
    private readonly IWaitingListRepository _repo;

    public WaitingListService(IWaitingListRepository repo) => _repo = repo;

    public WaitingListEntry Join(Guid concertId, Guid userId, int requestedSeatCount) =>
        _repo.Enqueue(concertId, userId, requestedSeatCount);

    public int GetPosition(Guid concertId, Guid userId) =>
        _repo.GetPosition(concertId, userId);
}
