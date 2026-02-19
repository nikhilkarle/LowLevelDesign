using StackOverflow.Models;

namespace StackOverflow.Repositories;

public interface IAnswerRepository
{
    Answer? GetById(long id);
    IEnumerable<Answer> GetAll();
    void Save(Answer answer);
}