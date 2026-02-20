using VendingMachine.Domain;

namespace VendingMachine.Repositories;

public sealed class InMemoryInventoryRepository : IInventoryRepository
{
    private readonly Dictionary<string, Product> _products;

    public InMemoryInventoryRepository(IEnumerable<Product> products)
    {
        _products = products.ToDictionary(p => p.Id, p => p);
    }

    public Product? GetById(string productId)
        => _products.TryGetValue(productId, out var p) ? p : null;

    public IReadOnlyList<Product> GetAll() => _products.Values.ToList();

    public void AddStock(string productId, int qty)
    {
        if (!_products.TryGetValue(productId, out var p))
            throw new InvalidOperationException($"Unknown product {productId}");
        p.AddStock(qty);
    }

    public void RemoveOne(string productId)
    {
        if (!_products.TryGetValue(productId, out var p))
            throw new InvalidOperationException($"Unknown product {productId}");
        p.RemoveOne();
    }
}
