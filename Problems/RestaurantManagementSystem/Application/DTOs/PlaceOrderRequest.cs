namespace RestaurantManagementSystem.Application.DTOs;

public class OrderItemRequest
{
    public Guid MenuItemId { get; init; }
    public int Quantity { get; init; }
    public string SpecialInstructions { get; init; } = "";
}

public class PlaceOrderRequest
{
    public Guid CustomerId { get; init; }
    public Guid TableId { get; init; }
    public List<OrderItemRequest> Items { get; init; } = new();
}
