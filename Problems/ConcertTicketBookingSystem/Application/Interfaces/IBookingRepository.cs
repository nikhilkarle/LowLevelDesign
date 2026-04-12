using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Interfaces;

public interface IBookingRepository
{
    Booking? GetById(Guid id);
    IEnumerable<Booking> GetByUser(Guid userId);
    void Save(Booking booking);
}
