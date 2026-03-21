using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Reservations;

public sealed class CheckedOutReservationState : IReservationState
{
    public ReservationStatus Status => ReservationStatus.CheckedOut;

    public void Confirm(Reservation reservation) => throw new InvalidOperationException("Checked-out reservation cannot be confirmed.");

    public void CheckIn(Reservation reservation) => throw new InvalidOperationException("Checked-out reservation cannot be checked in again.");

    public void CheckOut(Reservation reservation) => throw new InvalidOperationException("Reservation is already checked out.");

    public void Cancel(Reservation reservation) => throw new InvalidOperationException("Checked-out reservation cannot be cancelled.");
}