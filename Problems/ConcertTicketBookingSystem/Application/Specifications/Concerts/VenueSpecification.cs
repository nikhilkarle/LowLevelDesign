using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Specifications.Concerts;

public class VenueSpecification(Guid venueId) : Specification<Concert>
{
    public override bool IsSatisfiedBy(Concert concert) => concert.VenueId == venueId;
}
