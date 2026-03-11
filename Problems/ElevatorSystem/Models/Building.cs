namespace ElevatorSystem.Models;

public class Building
{
    public int TotalFloors { get; }
    public IReadOnlyList<Floor> Floors { get; }

    public Building(int totalFloors)
    {
        if (totalFloors <= 0)
            throw new ArgumentOutOfRangeException(nameof(totalFloors), "Total floors must be greater than 0.");

        TotalFloors = totalFloors;
        Floors = Enumerable.Range(1, totalFloors)
            .Select(f => new Floor(f))
            .ToList()
            .AsReadOnly();
    }

    public bool IsValidFloor(int floorNumber)
    {
        return floorNumber >= 1 && floorNumber <= TotalFloors;
    }
}