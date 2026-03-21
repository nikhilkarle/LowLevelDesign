namespace HotelManagementSystem.Domain.Entities;

public sealed class Guest
{
    public Guid Id { get; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    public Guest(Guid id, string fullName, string email, string phoneNumber)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void UpdateContact(string email, string phoneNumber)
    {
        Email = email;
        PhoneNumber = phoneNumber;
    }
}