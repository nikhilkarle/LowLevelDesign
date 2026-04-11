using RestaurantManagementSystem.Domain.Entities;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface ITableRepository
{
    void Add(Table table);
    Table? GetById(Guid id);
    IReadOnlyList<Table> GetAll();
    IReadOnlyList<Table> GetAvailable(int minCapacity);
}
