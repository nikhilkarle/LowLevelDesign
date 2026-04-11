using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Reservations;

public interface IReservationState
{
    ReservationStatus Status { get; }
    void Confirm(Reservation reservation);
    void Complete(Reservation reservation);
    void Cancel(Reservation reservation);
}
