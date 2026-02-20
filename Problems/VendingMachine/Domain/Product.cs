namespace VendingMachine.Domain;

public sealed class Product
{
    public string Id {get;}
    public string Name{get;}
    public int PriceCents { get; }
    public int Quantity { get; private set; }

    public Product(string id, string name, int priceCents, int quantity)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("id required");
        if (priceCents <= 0) throw new ArgumentException("priceCents must be > 0");
        if (quantity < 0) throw new ArgumentException("quantity cannot be negative");

        Id = id;
        Name = name;
        PriceCents = priceCents;
        Quantity = quantity;
    }

    public bool IsInStock => Quantity > 0;

    public void AddStock(int qnt)
    {
        if (qnt < 0) throw new ArgumentException("quantity cannot be <= 0");
        Quantity += qnt;
    }

    public void RemoveOne()
    {
        if (Quantity < 0) throw new ArgumentException("Product Out of Stock");
        Quantity -= 1;
    }

    public override string ToString() => $"{Id} - {Name} (${PriceCents/100.0:0.00}) x{Quantity}";
}