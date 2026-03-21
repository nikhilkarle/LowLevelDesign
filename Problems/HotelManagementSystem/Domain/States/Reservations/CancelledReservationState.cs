using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Domain.Enums;

namespace HotelManagementSystem.Domain.States.Reservations;

public sealed class CancelledReservationState : IReservationState
{
    public ReservationStatus Status => ReservationStatus.Cancelled;

    public void Confirm(Reservation reservation) => throw new InvalidOperationException("Cancelled reservation cannot be confirmed.");

    public void CheckIn(Reservation reservation) => throw new InvalidOperationException("Cancelled reservation cannot be checked in.");

    public void CheckOut(Reservation reservation) => throw new InvalidOperationException("Cancelled reservation cannot be checked out.");

    public void Cancel(Reservation reservation) => throw new InvalidOperationException("Reservation is already cancelled.");
}