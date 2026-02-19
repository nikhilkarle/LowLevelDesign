using StackOverflow.Models;

namespace StackOverflow.Factories;

public interface ICommentFactory
{
    Comment Create(long authorId, CommentTarget target, string content);
}
