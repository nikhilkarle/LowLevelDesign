using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Reservations;

public sealed class PendingReservationState : IReservationState
{
    public ReservationStatus Status => ReservationStatus.Pending;

    public void Confirm(Reservation reservation) => reservation.ChangeState(new ConfirmedReservationState());

    public void CheckIn(Reservation reservation) => throw new InvalidOperationException("Pending reservation must be confirmed before check-in.");

    public void CheckOut(Reservation reservation) => throw new InvalidOperationException("Cannot check out a pending reservation.");

    public void Cancel(Reservation reservation) => reservation.ChangeState(new CancelledReservationState());
}