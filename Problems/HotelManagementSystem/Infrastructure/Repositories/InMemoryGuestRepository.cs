using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Infrastructure.Interfaces;

namespace HotelManagementSystem.Infrastructure.Repositories;

public sealed class InMemoryGuestRepository : IGuestRepository
{
    private readonly Dictionary<Guid, Guest> _guests = new();

    public Guest? GetById(Guid guestId) => _guests.GetValueOrDefault(guestId);

    public void Add(Guest guest) => _guests[guest.Id] = guest;

    public void Update(Guest guest) => _guests[guest.Id] = guest;
}