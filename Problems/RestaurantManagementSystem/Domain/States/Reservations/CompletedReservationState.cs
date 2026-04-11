using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.States.Reservations;

public sealed class CompletedReservationState : IReservationState
{
    public ReservationStatus Status => ReservationStatus.Completed;

    public void Confirm(Reservation reservation) => throw new InvalidOperationException("Reservation is already completed.");
    public void Complete(Reservation reservation) => throw new InvalidOperationException("Reservation is already completed.");
    public void Cancel(Reservation reservation)  => throw new InvalidOperationException("Cannot cancel a completed reservation.");
}
