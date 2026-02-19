using StackOverflow.Models;

namespace StackOverflow.Factories;

public interface IQuestionFactory
{
    Question Create(long authorId, string content, IEnumerable<string> tags, IEnumerable<string> keywords);
}
