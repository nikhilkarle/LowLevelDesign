using Facebook.Domain.Entities;

namespace Facebook.Application.Interfaces;

public interface IUserRepository
{
    void Add(User user);
    User? GetById(Guid id);
    User? GetByEmail(string email);
    IReadOnlyList<User> GetAll();
}
