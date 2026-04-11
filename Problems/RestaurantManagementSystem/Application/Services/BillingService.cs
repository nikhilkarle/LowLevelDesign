using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Application.Services;

public class BillingService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly Dictionary<PaymentMethod, IPaymentProcessor> _processors;

    public BillingService(
        IInvoiceRepository invoiceRepository,
        IPaymentRepository paymentRepository,
        Dictionary<PaymentMethod, IPaymentProcessor> processors)
    {
        _invoiceRepository = invoiceRepository;
        _paymentRepository = paymentRepository;
        _processors        = processors;
    }

    public Invoice GenerateInvoice(Order order)
    {
        var lines = order.Items
            .Select(i => new InvoiceLine($"{i.MenuItemName} x{i.Quantity}", i.Subtotal))
            .ToList();

        var invoice = new Invoice(Guid.NewGuid(), order.Id, lines);
        _invoiceRepository.Add(invoice);
        return invoice;
    }

    public Payment ProcessPayment(PaymentRequest request)
    {
        var invoice = _invoiceRepository.GetById(request.InvoiceId)
            ?? throw new InvalidOperationException($"Invoice {request.InvoiceId} not found.");

        if (!_processors.TryGetValue(request.Method, out var processor))
            throw new NotSupportedException($"Payment method {request.Method} is not supported.");

        var payment = new Payment(Guid.NewGuid(), invoice.Id, invoice.TotalAmount, request.Method);

        if (processor.Process(payment))
            payment.Complete();
        else
            payment.Fail();

        _paymentRepository.Add(payment);
        return payment;
    }

    public Invoice GetInvoiceByOrder(Guid orderId)
        => _invoiceRepository.GetByOrderId(orderId)
           ?? throw new InvalidOperationException($"No invoice found for order {orderId}.");
}
