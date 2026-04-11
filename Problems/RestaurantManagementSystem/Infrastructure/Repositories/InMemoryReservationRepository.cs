using System.Collections.Concurrent;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Infrastructure.Repositories;

public class InMemoryReservationRepository : IReservationRepository
{
    private readonly ConcurrentDictionary<Guid, Reservation> _store = new();

    public void Add(Reservation reservation) => _store[reservation.Id] = reservation;

    public Reservation? GetById(Guid id) => _store.GetValueOrDefault(id);

    public IReadOnlyList<Reservation> GetByDate(DateTime date)
        => _store.Values.Where(r => r.Date.Date == date.Date).ToList();

    public IReadOnlyList<Reservation> GetByCustomer(Guid customerId)
        => _store.Values.Where(r => r.CustomerId == customerId).ToList();
}
