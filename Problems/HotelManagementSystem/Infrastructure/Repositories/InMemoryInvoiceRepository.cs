using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Infrastructure.Interfaces;

namespace HotelManagementSystem.Infrastructure.Repositories;

public sealed class InMemoryInvoiceRepository : IInvoiceRepository
{
    private readonly Dictionary<Guid, Invoice> _invoicesByReservationId = new();

    public Invoice? GetByReservationId(Guid reservationId) =>
        _invoicesByReservationId.GetValueOrDefault(reservationId);

    public void Add(Invoice invoice) => _invoicesByReservationId[invoice.ReservationId] = invoice;

    public void Update(Invoice invoice) => _invoicesByReservationId[invoice.ReservationId] = invoice;
}