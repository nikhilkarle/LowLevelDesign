namespace ConcertTicketBookingSystem.Domain.Entities;

public class Concert
{
    public Guid     Id         { get; }
    public string   ArtistName { get; }
    public Guid     VenueId    { get; }
    public DateTime DateTime   { get; }

    public Concert(Guid id, string artistName, Guid venueId, DateTime dateTime)
    {
        Id = id; ArtistName = artistName; VenueId = venueId; DateTime = dateTime;
    }
}
