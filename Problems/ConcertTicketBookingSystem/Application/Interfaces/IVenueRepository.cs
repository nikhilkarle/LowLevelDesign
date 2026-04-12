using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Interfaces;

public interface IVenueRepository
{
    Venue? GetById(Guid id);
    IEnumerable<Venue> GetAll();
    void Add(Venue venue);
}
