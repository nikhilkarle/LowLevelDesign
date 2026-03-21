using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Infrastructure.Interfaces;

namespace HotelManagementSystem.Infrastructure.Repositories;

public sealed class InMemoryReservationRepository : IReservationRepository
{
    private readonly Dictionary<Guid, Reservation> _reservations = new();

    public Reservation? GetById(Guid reservationId) => _reservations.GetValueOrDefault(reservationId);

    public IReadOnlyList<Reservation> GetAll() => _reservations.Values.ToList();

    public IReadOnlyList<Reservation> GetByGuest(Guid guestId) =>
        _reservations.Values.Where(x => x.GuestId == guestId).ToList();

    public IReadOnlyList<Reservation> GetActiveReservationsForRoom(Guid roomId) =>
        _reservations.Values
            .Where(x => x.AssignedRoomId == roomId &&
                        x.Status != ReservationStatus.Cancelled &&
                        x.Status != ReservationStatus.CheckedOut)
            .ToList();

    public void Add(Reservation reservation) => _reservations[reservation.Id] = reservation;

    public void Update(Reservation reservation) => _reservations[reservation.Id] = reservation;
}