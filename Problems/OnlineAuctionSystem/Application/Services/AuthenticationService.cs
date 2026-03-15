using OAS.Application.Interfaces;
using OAS.Domain.Entities;
using OAS.Infrastructure.Repositories;

namespace OAS.Application.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Register(string userName, string email, string password)
    {
        if (_userRepository.GetByEmail(email) != null)
            throw new InvalidOperationException("User already exists.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User(Guid.NewGuid(), userName, email, passwordHash);
        _userRepository.Add(user);
        return user;
    }

    public User Login(string email, string password)
    {
        var user = _userRepository.GetByEmail(email)
                   ?? throw new InvalidOperationException("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new InvalidOperationException("Invalid credentials.");

        return user;
    }
}