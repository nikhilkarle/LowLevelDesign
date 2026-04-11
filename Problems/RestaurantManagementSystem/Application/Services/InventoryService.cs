using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Services;

public class InventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly object _lock = new();

    public InventoryService(IInventoryRepository inventoryRepository)
        => _inventoryRepository = inventoryRepository;

    public InventoryItem AddItem(string name, double quantity, string unit, double reorderThreshold)
    {
        var item = new InventoryItem(Guid.NewGuid(), name, quantity, unit, reorderThreshold);
        _inventoryRepository.Add(item);
        return item;
    }

    public bool CanFulfill(MenuItem menuItem, int servings)
    {
        lock (_lock)
        {
            foreach (var (ingredientId, qtyPerServing) in menuItem.RequiredIngredients)
            {
                var ingredient = GetOrThrow(ingredientId);
                if (!ingredient.HasSufficientStock(qtyPerServing * servings))
                    return false;
            }
            return true;
        }
    }

    public void DeductIngredients(MenuItem menuItem, int servings)
    {
        lock (_lock)
        {
            foreach (var (ingredientId, qtyPerServing) in menuItem.RequiredIngredients)
            {
                var ingredient = GetOrThrow(ingredientId);
                ingredient.Deduct(qtyPerServing * servings);

                if (ingredient.NeedsReorder)
                    Console.WriteLine($"  [Inventory Alert] '{ingredient.Name}' is low: {ingredient.Quantity} {ingredient.Unit} remaining.");
            }
        }
    }

    public void RestoreIngredients(MenuItem menuItem, int servings)
    {
        lock (_lock)
        {
            foreach (var (ingredientId, qtyPerServing) in menuItem.RequiredIngredients)
            {
                var ingredient = GetOrThrow(ingredientId);
                ingredient.Restock(qtyPerServing * servings);
            }
        }
    }

    public void Restock(Guid ingredientId, double amount)
    {
        lock (_lock)
        {
            var ingredient = GetOrThrow(ingredientId);
            ingredient.Restock(amount);
        }
    }

    public IReadOnlyList<InventoryItem> GetLowStockItems() => _inventoryRepository.GetLowStockItems();

    private InventoryItem GetOrThrow(Guid id)
        => _inventoryRepository.GetById(id)
           ?? throw new InvalidOperationException($"Inventory item {id} not found.");
}
