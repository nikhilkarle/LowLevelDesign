using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Application.Interfaces;

public interface IStaffRepository
{
    void Add(Staff staff);
    Staff? GetById(Guid id);
    IReadOnlyList<Staff> GetAll();
    IReadOnlyList<Staff> GetByRole(StaffRole role);
}
