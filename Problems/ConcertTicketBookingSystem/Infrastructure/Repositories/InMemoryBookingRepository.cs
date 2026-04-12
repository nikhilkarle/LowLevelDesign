using System.Collections.Concurrent;
using ConcertTicketBookingSystem.Application.Interfaces;
using ConcertTicketBookingSystem.Domain.Entities;

namespace ConcertTicketBookingSystem.Infrastructure.Repositories;

public class InMemoryBookingRepository : IBookingRepository
{
    private readonly ConcurrentDictionary<Guid, Booking> _bookings = new();

    public Booking? GetById(Guid id)              => _bookings.GetValueOrDefault(id);
    public IEnumerable<Booking> GetByUser(Guid id) => _bookings.Values.Where(b => b.UserId == id);
    public void Save(Booking booking)              => _bookings[booking.Id] = booking;
}
