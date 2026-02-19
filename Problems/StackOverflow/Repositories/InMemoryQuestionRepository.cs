using StackOverflow.Models;

namespace StackOverflow.Repositories;

public sealed class InMemoryQuestionRepository : IQuestionRepository
{
    private readonly Dictionary<long, Question> _store = new();

    public Question? GetById(long id) => _store.TryGetValue(id, out var u) ? u : null;
    public IEnumerable<Question> GetAll() => _store.Values;
    public void Save(Question question) => _store[question.Id] = question;
}