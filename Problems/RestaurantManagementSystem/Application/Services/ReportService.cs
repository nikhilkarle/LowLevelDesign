using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Application.Strategies;

namespace RestaurantManagementSystem.Application.Services;

public class ReportService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IPaymentRepository _paymentRepository;
    private IReportStrategy _strategy;

    public ReportService(
        IOrderRepository orderRepository,
        IInventoryRepository inventoryRepository,
        IPaymentRepository paymentRepository,
        IReportStrategy strategy)
    {
        _orderRepository     = orderRepository;
        _inventoryRepository = inventoryRepository;
        _paymentRepository   = paymentRepository;
        _strategy            = strategy;
    }

    public void SetStrategy(IReportStrategy strategy) => _strategy = strategy;

    public ReportResult Generate(DateTime from, DateTime to)
        => _strategy.Generate(_orderRepository, _inventoryRepository, _paymentRepository, from, to);
}
