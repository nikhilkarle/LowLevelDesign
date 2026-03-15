namespace OAS.Domain.Entities;

public class User
{
    public Guid Id {get;}
    public string UserName { get; }
    public string Email { get; }
    public string PasswordHash { get; private set; }

    public User(Guid id, string userName, string email, string passwordHash)
    {
        Id = id;
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
    }
}