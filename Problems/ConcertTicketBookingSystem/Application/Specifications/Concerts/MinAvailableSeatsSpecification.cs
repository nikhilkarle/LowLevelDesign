using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Specifications.Concerts;

public class MinAvailableSeatsSpecification(int min, IConcertSeatRepository seatRepo) : Specification<Concert>
{
    public override bool IsSatisfiedBy(Concert concert) =>
        seatRepo.GetAvailableByConcert(concert.Id).Count() >= min;
}
