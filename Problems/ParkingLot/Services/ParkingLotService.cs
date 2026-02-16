using ParkingLot.Domain;
using ParkingLot.RealTime;
using ParkingLot.Strategies;

namespace ParkingLot.Services;

public sealed class ParkingLotService
{
    private readonly List<ParkingLevel> _levels;
    private readonly TicketStore _ticketStore;
    private readonly ISpotAssignmentStrategy _strategy;
    private readonly AvailabilityPublisher _publisher;

    public ParkingLotService(
        List<ParkingLevel> levels,
        TicketStore ticketStore,
        ISpotAssignmentStrategy strategy,
        AvailabilityPublisher publisher)
    {
        _levels = levels;
        _ticketStore = ticketStore;
        _strategy = strategy;
        _publisher = publisher;
    }

    public ParkingTicket Enter(Vehicle vehicle)
    {
        var allSpots = _levels.SelectMany(l => l.Spots).ToList();
        var candidate = _strategy.FindSpot(allSpots, vehicle) ?? throw new InvalidOperationException("Lot full for this vehicle type.");

        var level = _levels.First(l => l.LevelNumber == candidate.LevelNumber);
        if (!level.TryPark(candidate, vehicle)) throw new InvalidOperationException("Concurrent allocation happened; retry would be done in real system.");

        var ticket = new ParkingTicket(Guid.NewGuid().ToString("N"), vehicle, candidate);
        if (!_ticketStore.TryAdd(ticket)) throw new InvalidOperationException("Ticket creation failed.");

        PublishAvailability();
        return ticket;
    }

    public void Exit(string ticketId)
    {
        if (!_ticketStore.TryRemove(ticketId, out var ticket) || ticket is null) throw new KeyNotFoundException("Invalid ticket.");

        var level = _levels.First(l => l.LevelNumber == ticket.LevelNumber);
        if (!level.TryUnpark(ticket.SpotId)) throw new KeyNotFoundException("Spot already free/ not found.");

        ticket.Close();
        PublishAvailability();
    }

    void PublishAvailability() => _publisher.Publish(GetAvailability());

    public AvailabilitySnapshot GetAvailability()
    {
        var allSpots = _levels.SelectMany(l => l.Spots);

        return new AvailabilitySnapshot(
            TotalFreeMotorcycle : allSpots.Count(s => s.IsFree && s.SpotType == SpotType.Motorcycle),
            TotalFreeCompact : allSpots.Count(s => s.IsFree && s.SpotType == SpotType.Compact),
            TotalFreeLarge : allSpots.Count(s => s.IsFree && s.SpotType == SpotType.Large)
        );
    }

}