using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Reservations;

public sealed class ConfirmedReservationState : IReservationState
{
    public ReservationStatus Status => ReservationStatus.Confirmed;

    public void Confirm(Reservation reservation) => throw new InvalidOperationException("Reservation is already confirmed.");

    public void CheckIn(Reservation reservation) => reservation.ChangeState(new CheckedInReservationState());

    public void CheckOut(Reservation reservation) => throw new InvalidOperationException("Cannot check out before check-in.");

    public void Cancel(Reservation reservation) => reservation.ChangeState(new CancelledReservationState());
}