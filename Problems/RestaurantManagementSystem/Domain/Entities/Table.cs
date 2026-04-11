namespace RestaurantManagementSystem.Domain.Entities;

public class Table
{
    public Guid Id { get; }
    public int Number { get; }
    public int Capacity { get; }
    public bool IsAvailable { get; private set; }

    public Table(Guid id, int number, int capacity)
    {
        Id = id;
        Number = number;
        Capacity = capacity;
        IsAvailable = true;
    }

    public void Reserve() => IsAvailable = false;
    public void Release() => IsAvailable = true;
}
