using StackOverflow.Models;

namespace StackOverflow.Repositories;

public interface IQuestionRepository
{
    Question? GetById(long id);
    IEnumerable<Question> GetAll();
    void Save(Question question);
}