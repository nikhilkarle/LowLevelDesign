namespace HotelManagementSystem.Domain.Entities;

public sealed class ChargeItem
{
    public Guid Id { get; }
    public string Description { get; }
    public decimal Amount { get; }

    public ChargeItem(Guid id, string description, decimal amount)
    {
        Id = id;
        Description = description;
        Amount = amount;
    }
}