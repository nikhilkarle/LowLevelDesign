using System.Collections.Concurrent;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Infrastructure.Repositories;

public class InMemoryVenueRepository : IVenueRepository
{
    private readonly ConcurrentDictionary<Guid, Venue> _venues = new();

    public Venue? GetById(Guid id)            => _venues.GetValueOrDefault(id);
    public IEnumerable<Venue> GetAll()        => _venues.Values;
    public void Add(Venue venue)              => _venues[venue.Id] = venue;
}
