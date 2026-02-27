using System.Collections.Concurrent;
using TaskManagementSystem.Application.Interfaces;

namespace TaskManagementSystem.Infrastructure.Concurrency;

public sealed class InMemoryTaskLockProvider : ITaskLockProvider
{
    private readonly ConcurrentDictionary<Guid, object> _locks = new();

    public IDisposable Acquire(Guid taskId)
    {
        object gate = _locks.GetOrAdd(taskId, _ => new object());
        Monitor.Enter(gate);
        return new Releaser(gate);
    }

    private sealed class Releaser : IDisposable
    {
        private readonly object _gate;
        private bool _disposed;

        public Releaser(object gate) => _gate = gate;

        public void Dispose()
        {
            if (_disposed) return;
            Monitor.Exit(_gate);
            _disposed = true;
        }
    }
}
