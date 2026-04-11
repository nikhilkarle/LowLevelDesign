using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface IMenuItemRepository
{
    void Add(MenuItem item);
    MenuItem? GetById(Guid id);
    IReadOnlyList<MenuItem> GetAll();
    IReadOnlyList<MenuItem> GetByCategory(MenuCategory category);
    IReadOnlyList<MenuItem> GetAvailable();
}
