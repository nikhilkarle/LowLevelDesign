using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface IInventoryRepository
{
    void Add(InventoryItem item);
    InventoryItem? GetById(Guid id);
    IReadOnlyList<InventoryItem> GetAll();
    IReadOnlyList<InventoryItem> GetLowStockItems();
}
