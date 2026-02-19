using StackOverflow.Models;

namespace StackOverflow.Repositories;

public interface IUserRepository
{
    User? GetById(long id);
    IEnumerable<User> GetAll();
    void Save(User user);
}