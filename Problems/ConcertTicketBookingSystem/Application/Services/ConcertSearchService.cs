using ConcertTicketBookingSystem.Application.DTOs;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Application.Specifications;
using ConcertTicketBookingSystem.Application.Specifications.Concerts;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Services;

public class ConcertSearchService(
    IConcertRepository concertRepo,
    IConcertSeatRepository seatRepo,
    IVenueRepository venueRepo)
{
    private readonly ConcertSpecificationBuilder _builder = new(seatRepo);

    public IEnumerable<Concert> Search(ISpecification<Concert> spec) =>
        concertRepo.Search(spec);

    public IEnumerable<Concert> Search(SearchCriteria criteria) =>
        concertRepo.Search(_builder.Build(criteria));

    public IEnumerable<ConcertSeat> GetAvailableSeats(Guid concertId) =>
        seatRepo.GetAvailableByConcert(concertId);

    public Venue? GetVenue(Guid venueId) => venueRepo.GetById(venueId);
}
