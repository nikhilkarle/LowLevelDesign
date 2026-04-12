using System.Collections.Concurrent;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Infrastructure.Repositories;

public class InMemoryWaitingListRepository : IWaitingListRepository
{
    private readonly ConcurrentDictionary<Guid, Queue<WaitingListEntry>> _queues = new();
    private int _positionSeed;
    private readonly object _lock = new();

    public WaitingListEntry Enqueue(Guid concertId, Guid userId, int requestedSeatCount)
    {
        lock (_lock)
        {
            var position = Interlocked.Increment(ref _positionSeed);
            var entry    = new WaitingListEntry(Guid.NewGuid(), userId, concertId, requestedSeatCount, position);
            _queues.GetOrAdd(concertId, _ => new Queue<WaitingListEntry>()).Enqueue(entry);
            return entry;
        }
    }

    public WaitingListEntry? Dequeue(Guid concertId)
    {
        lock (_lock)
        {
            return _queues.TryGetValue(concertId, out var q) && q.Count > 0
                ? q.Dequeue()
                : null;
        }
    }

    public int GetPosition(Guid concertId, Guid userId)
    {
        if (!_queues.TryGetValue(concertId, out var q)) return -1;
        var list = q.ToList();
        var idx  = list.FindIndex(e => e.UserId == userId);
        return idx >= 0 ? idx + 1 : -1;
    }
}
