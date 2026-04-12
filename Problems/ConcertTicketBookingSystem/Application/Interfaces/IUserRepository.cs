using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Application.Interfaces;

public interface IUserRepository
{
    User? GetById(Guid id);
    void Add(User user);
}
