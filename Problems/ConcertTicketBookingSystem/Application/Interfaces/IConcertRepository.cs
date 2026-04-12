using ConcertTicketBookingSystem.Application.Specifications;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Interfaces;

public interface IConcertRepository
{
    Concert? GetById(Guid id);
    IEnumerable<Concert> Search(ISpecification<Concert> spec);
    void Add(Concert concert);
}
