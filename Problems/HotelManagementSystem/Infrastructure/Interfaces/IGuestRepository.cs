using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Infrastructure.Interfaces;

public interface IGuestRepository
{
    Guest? GetById(Guid guestId);
    void Add(Guest guest);
    void Update(Guest guest);
}