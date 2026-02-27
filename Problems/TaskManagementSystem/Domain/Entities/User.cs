namespace TaskManagementSystem.Domain.Entities;

public sealed class User
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    public User(Guid id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}
