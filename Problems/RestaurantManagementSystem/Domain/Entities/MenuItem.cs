using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public MenuCategory Category { get; private set; }
    public bool IsAvailable { get; private set; }

    private readonly Dictionary<Guid, double> _requiredIngredients;
    public IReadOnlyDictionary<Guid, double> RequiredIngredients => _requiredIngredients;

    public MenuItem(Guid id, string name, string description, decimal price,
        MenuCategory category, Dictionary<Guid, double> requiredIngredients)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        IsAvailable = true;
        _requiredIngredients = requiredIngredients;
    }

    public void SetAvailability(bool isAvailable) => IsAvailable = isAvailable;
    public void UpdatePrice(decimal price) => Price = price;
}
