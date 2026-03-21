using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Infrastructure.Interfaces;

public interface IInvoiceRepository
{
    Invoice? GetByReservationId(Guid reservationId);
    void Add(Invoice invoice);
    void Update(Invoice invoice);
}