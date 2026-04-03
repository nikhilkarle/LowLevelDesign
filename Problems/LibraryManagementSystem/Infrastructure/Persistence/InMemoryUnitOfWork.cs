using LibraryManagementSystem.Application.Interfaces;

namespace LibraryManagementSystem.Infrastructure.Persistence
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public void Commit()
        {
            // No-op for in-memory implementation.
            // In a real system this would commit a database transaction.
        }
    }
}