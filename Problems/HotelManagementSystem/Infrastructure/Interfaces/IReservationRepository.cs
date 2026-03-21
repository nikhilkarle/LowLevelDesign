using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Infrastructure.Interfaces;

public interface IReservationRepository
{
    Reservation? GetById(Guid reservationId);
    IReadOnlyList<Reservation> GetAll();
    IReadOnlyList<Reservation> GetByGuest(Guid guestId);
    IReadOnlyList<Reservation> GetActiveReservationsForRoom(Guid roomId);
    void Add(Reservation reservation);
    void Update(Reservation reservation);
}