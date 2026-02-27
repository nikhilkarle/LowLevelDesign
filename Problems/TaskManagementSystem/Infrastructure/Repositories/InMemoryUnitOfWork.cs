using TaskManagementSystem.Application.Interfaces;

namespace TaskManagementSystem.Infrastructure.Persistence;

public sealed class InMemoryUnitOfWork : IUnitOfWork
{
    public void Commit()
    {
    }
}