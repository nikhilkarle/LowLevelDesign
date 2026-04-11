using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Reservations;

public sealed class ConfirmedReservationState : IReservationState
{
    public ReservationStatus Status => ReservationStatus.Confirmed;

    public void Confirm(Reservation reservation) => throw new InvalidOperationException("Reservation is already confirmed.");
    public void Complete(Reservation reservation) => reservation.ChangeState(new CompletedReservationState());
    public void Cancel(Reservation reservation)  => reservation.ChangeState(new CancelledReservationState());
}
