using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Specifications.Concerts;

public class MaxPriceSpecification(decimal maxPrice, IConcertSeatRepository seatRepo) : Specification<Concert>
{
    public override bool IsSatisfiedBy(Concert concert) =>
        seatRepo.GetAvailableByConcert(concert.Id).Any(s => s.Price <= maxPrice);
}
