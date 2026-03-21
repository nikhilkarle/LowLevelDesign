using HotelManagementSystem.Application.Interfaces;

namespace HotelManagementSystem.Application.Services;

public sealed class NotificationService : INotificationService
{
    public void SendReservationConfirmation(Guid guestId, Guid reservationId)
    {
        Console.WriteLine($"Reservation confirmation sent to guest {guestId} for reservation {reservationId}");
    }

    public void SendCheckOutReceipt(Guid guestId, Guid reservationId)
    {
        Console.WriteLine($"Check-out receipt sent to guest {guestId} for reservation {reservationId}");
    }
}