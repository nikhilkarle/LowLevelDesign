using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Reservations;

public interface IReservationState
{
    ReservationStatus Status { get; }

    void Confirm(Reservation reservation);
    void CheckIn(Reservation reservation);
    void CheckOut(Reservation reservation);
    void Cancel(Reservation reservation);
}