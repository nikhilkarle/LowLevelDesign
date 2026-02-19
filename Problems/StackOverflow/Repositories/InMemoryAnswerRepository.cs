using StackOverflow.Models;

namespace StackOverflow.Repositories;

public sealed class InMemoryAnswerRepository : IAnswerRepository
{
    private readonly Dictionary<long, Answer> _store = new();

    public Answer? GetById(long id) => _store.TryGetValue(id, out var u) ? u : null;
    public IEnumerable<Answer> GetAll() => _store.Values;
    public void Save(Answer answer) => _store[answer.Id] = answer;
} 