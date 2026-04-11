using RestaurantManagementSystem.Application.Interfaces;
using RestaurantManagementSystem.Domain.Entities;
using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Application.Services;

public class StaffService
{
    private readonly IStaffRepository _staffRepository;

    public StaffService(IStaffRepository staffRepository)
        => _staffRepository = staffRepository;

    public Staff AddStaff(string name, string email, StaffRole role)
    {
        var staff = new Staff(Guid.NewGuid(), name, email, role);
        _staffRepository.Add(staff);
        return staff;
    }

    public void AddShift(Guid staffId, DateTime start, DateTime end)
    {
        var staff = GetOrThrow(staffId);
        staff.AddShift(new Shift(start, end));
    }

    public void UpdatePerformanceScore(Guid staffId, double score)
    {
        var staff = GetOrThrow(staffId);
        staff.UpdatePerformanceScore(score);
    }

    public IReadOnlyList<Staff> GetByRole(StaffRole role) => _staffRepository.GetByRole(role);

    public IReadOnlyList<Staff> GetAll() => _staffRepository.GetAll();

    private Staff GetOrThrow(Guid id)
        => _staffRepository.GetById(id)
           ?? throw new InvalidOperationException($"Staff {id} not found.");
}
