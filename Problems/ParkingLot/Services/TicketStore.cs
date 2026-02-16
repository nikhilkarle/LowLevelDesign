using System.Collections.Concurrent;
using ParkingLot.Domain;

namespace ParkingLot.Services;

public sealed class TicketStore
{
    private readonly ConcurrentDictionary<string, ParkingTicket> _active = new();
    public bool TryAdd(ParkingTicket t) => _active.TryAdd(t.TicketId, t);
    public bool TryRemove(string ticketId, out ParkingTicket? ticket) => _active.TryRemove(ticketId, out ticket);
}