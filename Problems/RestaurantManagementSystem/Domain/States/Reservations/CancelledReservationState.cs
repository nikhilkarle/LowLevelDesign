using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Reservations;

public sealed class CancelledReservationState : IReservationState
{
    public ReservationStatus Status => ReservationStatus.Cancelled;

    public void Confirm(Reservation reservation) => throw new InvalidOperationException("Cannot confirm a cancelled reservation.");
    public void Complete(Reservation reservation) => throw new InvalidOperationException("Cannot complete a cancelled reservation.");
    public void Cancel(Reservation reservation)  => throw new InvalidOperationException("Reservation is already cancelled.");
}
