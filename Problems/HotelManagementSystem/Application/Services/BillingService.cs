using HotelManagementSystem.Application.Interfaces;
using HotelManagementSystem.Domain.Entities;
using HotelManagementSystem.Infrastructure.Interfaces;

namespace HotelManagementSystem.Application.Services;

public sealed class BillingService : IBillingService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public BillingService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public Invoice CreateInvoice(Guid reservationId, decimal initialRoomCharge)
    {
        var invoice = new Invoice(Guid.NewGuid(), reservationId);
        invoice.AddCharge("Room Charge", initialRoomCharge);
        _invoiceRepository.Add(invoice);
        return invoice;
    }

    public void AddCharge(Guid reservationId, string description, decimal amount)
    {
        var invoice = _invoiceRepository.GetByReservationId(reservationId)
                     ?? throw new InvalidOperationException("Invoice not found.");

        invoice.AddCharge(description, amount);
        _invoiceRepository.Update(invoice);
    }

    public Invoice GetInvoice(Guid reservationId)
    {
        return _invoiceRepository.GetByReservationId(reservationId)
               ?? throw new InvalidOperationException("Invoice not found.");
    }

    public decimal CloseInvoice(Guid reservationId)
    {
        var invoice = GetInvoice(reservationId);
        invoice.Close();
        _invoiceRepository.Update(invoice);
        return invoice.GetTotal();
    }
}