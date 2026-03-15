using OAS.Domain.Entities;

namespace OAS.Infrastructure.Repositories;

public interface IUserRepository
{
    void Add(User user);
    User? GetById(Guid id);
    User? GetByEmail(string email);
}