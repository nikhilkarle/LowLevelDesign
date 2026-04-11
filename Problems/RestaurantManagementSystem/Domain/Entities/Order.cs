using RestaurantManagementSystem.Domain.Enums;
using RestaurantManagementSystem.Domain.States.Orders;

namespace RestaurantManagementSystem.Domain.Entities;

public class Order
{
    public Guid Id { get; }
    public Guid CustomerId { get; }
    public Guid TableId { get; }
    public OrderStatus Status => _state.Status;
    public DateTime CreatedAt { get; }
    public DateTime? CompletedAt { get; private set; }

    private IOrderState _state;
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items;
    public decimal TotalAmount => _items.Sum(i => i.Subtotal);

    public Order(Guid id, Guid customerId, Guid tableId)
    {
        Id = id;
        CustomerId = customerId;
        TableId = tableId;
        CreatedAt = DateTime.UtcNow;
        _state = new PendingOrderState();
    }

    public void AddItem(OrderItem item) => _items.Add(item);

    public void StartPreparing() => _state.StartPreparing(this);
    public void MarkReady()      => _state.MarkReady(this);
    public void MarkServed()
    {
        _state.MarkServed(this);
        CompletedAt = DateTime.UtcNow;
    }
    public void Cancel() => _state.Cancel(this);

    internal void ChangeState(IOrderState newState) => _state = newState;
}
