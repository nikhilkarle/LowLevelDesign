using StackOverflow.Models;

namespace StackOverflow.Repositories;

public interface ICommentRepository
{
    Comment? GetById(long id);
    IEnumerable<Comment> GetAll();
    void Save(Comment comment);
}