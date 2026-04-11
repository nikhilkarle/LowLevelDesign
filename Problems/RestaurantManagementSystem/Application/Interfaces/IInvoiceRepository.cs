using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface IInvoiceRepository
{
    void Add(Invoice invoice);
    Invoice? GetById(Guid id);
    Invoice? GetByOrderId(Guid orderId);
}
