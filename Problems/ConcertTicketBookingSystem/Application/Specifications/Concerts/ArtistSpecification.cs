using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Specifications.Concerts;

public class ArtistSpecification(string artist) : Specification<Concert>
{
    public override bool IsSatisfiedBy(Concert concert) =>
        concert.ArtistName.Contains(artist, StringComparison.OrdinalIgnoreCase);
}
