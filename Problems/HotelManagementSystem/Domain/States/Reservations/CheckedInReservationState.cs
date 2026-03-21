using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Reservations;

public sealed class CheckedInReservationState : IReservationState
{
    public ReservationStatus Status => ReservationStatus.CheckedIn;

    public void Confirm(Reservation reservation) => throw new InvalidOperationException("Reservation is already checked in.");

    public void CheckIn(Reservation reservation) => throw new InvalidOperationException("Reservation is already checked in.");

    public void CheckOut(Reservation reservation) => reservation.ChangeState(new CheckedOutReservationState());

    public void Cancel(Reservation reservation) => throw new InvalidOperationException("Checked-in reservation cannot be cancelled.");
}