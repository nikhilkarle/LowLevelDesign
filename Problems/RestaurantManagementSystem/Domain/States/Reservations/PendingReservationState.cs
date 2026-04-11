using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Reservations;

public sealed class PendingReservationState : IReservationState
{
    public ReservationStatus Status => ReservationStatus.Pending;

    public void Confirm(Reservation reservation) => reservation.ChangeState(new ConfirmedReservationState());
    public void Complete(Reservation reservation) => throw new InvalidOperationException("Pending reservation must be confirmed before it can be completed.");
    public void Cancel(Reservation reservation)  => reservation.ChangeState(new CancelledReservationState());
}
