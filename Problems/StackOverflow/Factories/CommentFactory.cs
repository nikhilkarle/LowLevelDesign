using StackOverflow.Models;
using StackOverflow.Services;

namespace StackOverflow.Factories;

public sealed class CommentFactory : ICommentFactory
{
    public Comment Create(long authorId, CommentTarget target, string content)
    {
        return new Comment
        {
            Id = IdGen.NewId(),
            AuthorId = authorId,
            Target = target,
            Content = (content ?? "").Trim()
        };
    }
}
