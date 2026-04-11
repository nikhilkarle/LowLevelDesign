namespace RestaurantManagementSystem.Domain.Entities;

public class Customer
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public int LoyaltyPoints { get; private set; }
    public DateTime CreatedAt { get; }

    public Customer(Guid id, string name, string email, string phone)
    {
        Id = id;
        Name = name;
        Email = email;
        Phone = phone;
        LoyaltyPoints = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(string name, string email, string phone)
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public void AddLoyaltyPoints(int points) => LoyaltyPoints += points;
}
