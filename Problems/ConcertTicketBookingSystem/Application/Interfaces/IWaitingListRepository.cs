using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Interfaces;

public interface IWaitingListRepository
{
    WaitingListEntry Enqueue(Guid concertId, Guid userId, int requestedSeatCount);
    WaitingListEntry? Dequeue(Guid concertId);
    int GetPosition(Guid concertId, Guid userId);  
}
