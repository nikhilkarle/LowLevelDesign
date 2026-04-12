namespace ConcertTicketBookingSystem.Domain.Entities;

public class WaitingListEntry
{
    public Guid     Id                 { get; }
    public Guid     UserId             { get; }
    public Guid     ConcertId          { get; }
    public int      RequestedSeatCount { get; }
    public int      Position           { get; }   // 1-based FIFO position
    public DateTime CreatedAt          { get; }

    public WaitingListEntry(Guid id, Guid userId, Guid concertId, int requestedSeatCount, int position)
    {
        Id = id; UserId = userId; ConcertId = concertId;
        RequestedSeatCount = requestedSeatCount; Position = position;
        CreatedAt = DateTime.UtcNow;
    }
}
