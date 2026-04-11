using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface IReservationRepository
{
    void Add(Reservation reservation);
    Reservation? GetById(Guid id);
    IReadOnlyList<Reservation> GetByDate(DateTime date);
    IReadOnlyList<Reservation> GetByCustomer(Guid customerId);
}
