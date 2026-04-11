namespace RestaurantManagementSystem.Domain.Entities;

public class InventoryItem
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public double Quantity { get; private set; }
    public string Unit { get; private set; }
    public double ReorderThreshold { get; private set; }

    public bool NeedsReorder => Quantity <= ReorderThreshold;

    public InventoryItem(Guid id, string name, double quantity, string unit, double reorderThreshold)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Unit = unit;
        ReorderThreshold = reorderThreshold;
    }

    public bool HasSufficientStock(double required) => Quantity >= required;

    public void Deduct(double amount)
    {
        if (amount > Quantity)
            throw new InvalidOperationException($"Insufficient stock for '{Name}': need {amount} {Unit}, have {Quantity}.");
        Quantity -= amount;
    }

    public void Restock(double amount) => Quantity += amount;
}
