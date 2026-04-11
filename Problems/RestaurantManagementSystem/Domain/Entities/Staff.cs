using RestaurantManagementSystem.Domain.Enums;

namespace RestaurantManagementSystem.Domain.Entities;

public class Shift
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public Shift(DateTime start, DateTime end) { Start = start; End = end; }
}

public class Staff
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public StaffRole Role { get; private set; }
    public double PerformanceScore { get; private set; }
    public DateTime HiredAt { get; }

    private readonly List<Shift> _schedule = new();
    public IReadOnlyList<Shift> Schedule => _schedule;

    public Staff(Guid id, string name, string email, StaffRole role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
        PerformanceScore = 100.0;
        HiredAt = DateTime.UtcNow;
    }

    public void AddShift(Shift shift) => _schedule.Add(shift);
    public void UpdateRole(StaffRole role) => Role = role;
    public void UpdatePerformanceScore(double score) => PerformanceScore = Math.Clamp(score, 0.0, 100.0);
}
