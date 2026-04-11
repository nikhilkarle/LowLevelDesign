using RestaurantManagementSystem.Domain.Enums;
using RestaurantManagementSystem.Domain.States.Reservations;

namespace RestaurantManagementSystem.Domain.Entities;

public class Reservation
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public Guid? TableId { get; private set; }
    public int PartySize { get; }
    public DateTime Date { get; }
    public TimeSpan TimeSlot { get; }
    public ReservationStatus Status => _state.Status;
    public DateTime CreatedAt { get; }

    private IReservationState _state;

    public Reservation(Guid id, Guid customerId, int partySize, DateTime date, TimeSpan timeSlot)
    {
        Id = id;
        CustomerId = customerId;
        PartySize = partySize;
        Date = date;
        TimeSlot = timeSlot;
        CreatedAt = DateTime.UtcNow;
        _state = new PendingReservationState();
    }

    public void Confirm(Guid tableId)
    {
        TableId = tableId;
        _state.Confirm(this);
    }

    public void Complete() => _state.Complete(this);
    public void Cancel()   => _state.Cancel(this);

    // Called by state objects only
    internal void ChangeState(IReservationState newState) => _state = newState;
}
