using StackOverflow.Models;

namespace StackOverflow.Factories;

public interface IAnswerFactory
{
    Answer Create(long authorId, long questionId, string content, IEnumerable<string> keywords);
}
