using RestaurantManagementSystem.Application.DTOs;
using RestaurantManagementSystem.Application.Factories;
using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Application.Observers;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly InventoryService _inventoryService;
    private readonly BillingService _billingService;
    private readonly IOrderFactory _orderFactory;
    private readonly List<IOrderObserver> _observers = new();

    public OrderService(
        IOrderRepository orderRepository,
        IMenuItemRepository menuItemRepository,
        InventoryService inventoryService,
        BillingService billingService,
        IOrderFactory orderFactory)
    {
        _orderRepository     = orderRepository;
        _menuItemRepository  = menuItemRepository;
        _inventoryService    = inventoryService;
        _billingService      = billingService;
        _orderFactory        = orderFactory;
    }

    public void Subscribe(IOrderObserver observer)   => _observers.Add(observer);
    public void Unsubscribe(IOrderObserver observer) => _observers.Remove(observer);

    public Order PlaceOrder(PlaceOrderRequest request)
    {
        var order = _orderFactory.Create(request.CustomerId, request.TableId);

        foreach (var itemReq in request.Items)
        {
            var menuItem = GetMenuItemOrThrow(itemReq.MenuItemId);

            if (!menuItem.IsAvailable)
                throw new InvalidOperationException($"'{menuItem.Name}' is not currently available.");

            if (!_inventoryService.CanFulfill(menuItem, itemReq.Quantity))
                throw new InvalidOperationException($"Insufficient ingredients for '{menuItem.Name}'.");

            _inventoryService.DeductIngredients(menuItem, itemReq.Quantity);

            order.AddItem(new OrderItem(
                Guid.NewGuid(), menuItem.Id, menuItem.Name,
                menuItem.Price, itemReq.Quantity, itemReq.SpecialInstructions));
        }

        _orderRepository.Add(order);
        Notify(order);
        return order;
    }

    public void StartPreparing(Guid orderId)
    {
        var order = GetOrThrow(orderId);
        order.StartPreparing();
        Notify(order);
    }

    public void MarkReady(Guid orderId)
    {
        var order = GetOrThrow(orderId);
        order.MarkReady();
        Notify(order);
    }

    public void MarkServed(Guid orderId)
    {
        var order = GetOrThrow(orderId);
        order.MarkServed();
        _billingService.GenerateInvoice(order);
        Notify(order);
    }

    public void CancelOrder(Guid orderId, MenuService menuService)
    {
        var order = GetOrThrow(orderId);
        order.Cancel();

        foreach (var item in order.Items)
        {
            var menuItem = menuService.GetOrThrow(item.MenuItemId);
            _inventoryService.RestoreIngredients(menuItem, item.Quantity);
        }

        Notify(order);
    }

    public Order GetOrThrow(Guid orderId)
        => _orderRepository.GetById(orderId)
           ?? throw new InvalidOperationException($"Order {orderId} not found.");

    private MenuItem GetMenuItemOrThrow(Guid id)
        => _menuItemRepository.GetById(id)
           ?? throw new InvalidOperationException($"Menu item {id} not found.");

    private void Notify(Order order)
    {
        foreach (var observer in _observers)
            observer.OnOrderStatusChanged(order);
    }
}
