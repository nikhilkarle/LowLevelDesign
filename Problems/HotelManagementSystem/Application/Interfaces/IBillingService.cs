using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Application.Interfaces;

public interface IBillingService
{
    Invoice CreateInvoice(Guid reservationId, decimal initialRoomCharge);
    void AddCharge(Guid reservationId, string description, decimal amount);
    Invoice GetInvoice(Guid reservationId);
    decimal CloseInvoice(Guid reservationId);
}