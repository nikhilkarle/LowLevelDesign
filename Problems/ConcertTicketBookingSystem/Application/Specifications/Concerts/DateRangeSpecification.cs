using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Specifications.Concerts;

public class DateRangeSpecification(DateTime? from, DateTime? to) : Specification<Concert>
{
    public override bool IsSatisfiedBy(Concert concert) =>
        (from is null || concert.DateTime >= from) &&
        (to   is null || concert.DateTime <= to);
}
