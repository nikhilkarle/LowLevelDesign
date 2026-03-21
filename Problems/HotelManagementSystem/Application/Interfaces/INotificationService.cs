namespace HotelManagementSystem.Application.Interfaces;

public interface INotificationService
{
    void SendReservationConfirmation(Guid guestId, Guid reservationId);
    void SendCheckOutReceipt(Guid guestId, Guid reservationId);
}