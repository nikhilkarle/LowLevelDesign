using System.Collections.Concurrent;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Application.Specifications;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Infrastructure.Repositories;

public class InMemoryConcertRepository : IConcertRepository
{
    private readonly ConcurrentDictionary<Guid, Concert> _concerts = new();

    public Concert? GetById(Guid id) => _concerts.GetValueOrDefault(id);

    public IEnumerable<Concert> Search(ISpecification<Concert> spec) =>
        _concerts.Values.Where(spec.IsSatisfiedBy);

    public void Add(Concert concert) => _concerts[concert.Id] = concert;
}
