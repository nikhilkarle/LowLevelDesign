namespace RestaurantManagementSystem.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; }
    public Guid MenuItemId { get; }
    public string MenuItemName { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; }
    public string SpecialInstructions { get; }

    public decimal Subtotal => UnitPrice * Quantity;

    public OrderItem(Guid id, Guid menuItemId, string menuItemName, decimal unitPrice,
        int quantity, string specialInstructions = "")
    {
        Id = id;
        MenuItemId = menuItemId;
        MenuItemName = menuItemName;
        UnitPrice = unitPrice;
        Quantity = quantity;
        SpecialInstructions = specialInstructions;
    }
}
