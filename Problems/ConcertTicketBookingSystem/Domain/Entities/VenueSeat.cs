using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Domain.Entities;

public class VenueSeat
{
    public Guid     Id       { get; }
    public Guid     VenueId  { get; }
    public string   Section  { get; }
    public string   Row      { get; }
    public int      Number   { get; }
    public SeatType Type     { get; }

    public VenueSeat(Guid id, Guid venueId, string section, string row, int number, SeatType type)
    {
        Id = id; VenueId = venueId; Section = section; Row = row; Number = number; Type = type;
    }

    public override string ToString() => $"{Section} {Row}{Number} ({Type})";
}
