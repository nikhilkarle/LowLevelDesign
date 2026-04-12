using ConcertTicketBookingSystem.Domain.Enums;

namespace ConcertTicketBookingSystem.Domain.Entities;

public class Venue
{
    private readonly List<VenueSeat> _seats = [];

    public Guid   Id       { get; }
    public string Name     { get; }
    public string Address  { get; }
    public int    Capacity { get; }

    public IReadOnlyList<VenueSeat> Seats => _seats.AsReadOnly();

    public Venue(Guid id, string name, string address, int capacity)
    {
        Id = id; Name = name; Address = address; Capacity = capacity;
    }

    public VenueSeat AddSeat(string section, string row, int number, SeatType type)
    {
        var seat = new VenueSeat(Guid.NewGuid(), Id, section, row, number, type);
        _seats.Add(seat);
        return seat;
    }
}
