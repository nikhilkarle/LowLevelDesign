using ConcertTicketBookingSystem.Application.DTOs;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Specifications.Concerts;

public class ConcertSpecificationBuilder(IConcertSeatRepository seatRepo)
{
    public ISpecification<Concert> Build(SearchCriteria criteria)
    {
        ISpecification<Concert> spec = new AllSpecification<Concert>();

        if (!string.IsNullOrWhiteSpace(criteria.Artist))
            spec = spec.And(new ArtistSpecification(criteria.Artist));

        if (criteria.VenueId.HasValue)
            spec = spec.And(new VenueSpecification(criteria.VenueId.Value));

        if (criteria.FromDate.HasValue || criteria.ToDate.HasValue)
            spec = spec.And(new DateRangeSpecification(criteria.FromDate, criteria.ToDate));

        if (criteria.MaxPrice.HasValue)
            spec = spec.And(new MaxPriceSpecification(criteria.MaxPrice.Value, seatRepo));

        if (criteria.MinAvailableSeats.HasValue)
            spec = spec.And(new MinAvailableSeatsSpecification(criteria.MinAvailableSeats.Value, seatRepo));

        return spec;
    }
}
