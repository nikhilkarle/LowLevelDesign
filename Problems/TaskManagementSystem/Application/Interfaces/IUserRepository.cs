using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Interfaces;

public interface IUserRepository
{
    User? GetById(Guid userId);
    void Add(User user);
}
