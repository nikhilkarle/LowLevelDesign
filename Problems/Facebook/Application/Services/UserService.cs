using Facebook.Application.Interfaces;
using Facebook.Domain.Entities;

namespace Facebook.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Register(string name, string email, string passwordHash)
    {
        if (_userRepository.GetByEmail(email) is not null)
            throw new InvalidOperationException($"Email '{email}' is already registered.");

        var user = new User(Guid.NewGuid(), name, email, passwordHash);
        _userRepository.Add(user);
        return user;
    }

    public User Login(string email, string passwordHash)
    {
        var user = _userRepository.GetByEmail(email)
            ?? throw new InvalidOperationException("Invalid credentials.");

        if (user.PasswordHash != passwordHash)
            throw new InvalidOperationException("Invalid credentials.");

        return user;
    }

    public void UpdateProfile(Guid userId, string name, string bio, string profilePictureUrl, List<string> interests)
    {
        var user = GetOrThrow(userId);
        user.UpdateProfile(name, bio, profilePictureUrl, interests);
    }

    public User GetOrThrow(Guid userId)
        => _userRepository.GetById(userId)
           ?? throw new InvalidOperationException($"User {userId} not found.");
}
