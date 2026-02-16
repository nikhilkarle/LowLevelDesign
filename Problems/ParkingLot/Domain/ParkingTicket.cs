namespace ParkingLot.Domain;

public sealed record ParkingTicket
{
    public string TicketId { get; }
    public string LicensePlate { get; }
    public string SpotId { get; }
    public int LevelNumber { get; }
    public DateTimeOffset EntryTime { get; }
    public DateTimeOffset? ExitTime { get; private set; }
    public TicketStatus Status { get; private set; } = TicketStatus.Active;

    public ParkingTicket(string ticketId, Vehicle vehicle, ParkingSpot spot)
    {
        TicketId = ticketId;
        LicensePlate = vehicle.LicensePlate;
        SpotId = spot.Id;
        LevelNumber = spot.LevelNumber;
        EntryTime = DateTimeOffset.UtcNow;
    }

    public void Close()
    {
        Status = TicketStatus.Closed;
        ExitTime = DateTimeOffset.UtcNow;
    }
}