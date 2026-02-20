using VendingMachine.Domain;

namespace VendingMachine.Repositories;

public interface IInventoryRepository
{
    Product? GetById(string productId);
    IReadOnlyList<Product> GetAll();
    void AddStock(string productId, int qty);
    void RemoveOne(string productId);
}
