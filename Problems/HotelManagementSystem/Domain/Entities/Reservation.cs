using HotelManagementSystem.Domain.Enums;
using HotelManagementSystem.Domain.States.Reservations;

namespace HotelManagementSystem.Domain.Entities;

public sealed class Reservation
{
    public Guid Id { get; }
    public Guid GuestId { get; }
    public Guid? AssignedRoomId { get; private set; }
    public RoomType RequestedRoomType { get; }
    public DateTime CheckInDate { get; }
    public DateTime CheckOutDate { get; }
    public ReservationStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }
    public DateTime CreatedAt { get; }
    public int Version { get; private set; }

    private IReservationState _state;

    public Reservation(
        Guid id,
        Guid guestId,
        RoomType requestedRoomType,
        DateTime checkInDate,
        DateTime checkOutDate,
        decimal totalAmount)
    {
        if (checkOutDate <= checkInDate)
            throw new ArgumentException("Check-out date must be after check-in date.");

        Id = id;
        GuestId = guestId;
        RequestedRoomType = requestedRoomType;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        TotalAmount = totalAmount;
        CreatedAt = DateTime.UtcNow;
        Status = ReservationStatus.Pending;
        _state = new PendingReservationState();
        Version = 0;
    }

    public void AssignRoom(Guid roomId)
    {
        AssignedRoomId = roomId;
        Version++;
    }

    public void Confirm() => _state.Confirm(this);
    public void CheckIn() => _state.CheckIn(this);
    public void CheckOut() => _state.CheckOut(this);
    public void Cancel() => _state.Cancel(this);

    public void ChangeState(IReservationState state)
    {
        _state = state;
        Status = state.Status;
        Version++;
    }
}