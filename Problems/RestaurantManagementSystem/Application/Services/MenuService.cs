using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Application.Services;

public class MenuService
{
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuService(IMenuItemRepository menuItemRepository)
        => _menuItemRepository = menuItemRepository;

    public MenuItem AddItem(string name, string description, decimal price,
        MenuCategory category, Dictionary<Guid, double> requiredIngredients)
    {
        var item = new MenuItem(Guid.NewGuid(), name, description, price, category, requiredIngredients);
        _menuItemRepository.Add(item);
        return item;
    }

    public IReadOnlyList<MenuItem> GetMenu() => _menuItemRepository.GetAvailable();

    public IReadOnlyList<MenuItem> GetByCategory(MenuCategory category)
        => _menuItemRepository.GetByCategory(category);

    public void SetAvailability(Guid itemId, bool isAvailable)
    {
        var item = GetOrThrow(itemId);
        item.SetAvailability(isAvailable);
    }

    public void UpdatePrice(Guid itemId, decimal price)
    {
        var item = GetOrThrow(itemId);
        item.UpdatePrice(price);
    }

    public MenuItem GetOrThrow(Guid id)
        => _menuItemRepository.GetById(id)
           ?? throw new InvalidOperationException($"Menu item {id} not found.");
}
