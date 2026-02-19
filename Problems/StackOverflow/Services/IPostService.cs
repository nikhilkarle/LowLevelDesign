using StackOverflow.Models;

namespace StackOverflow.Services;

public interface IPostService
{
    long CreateQuestion(long userId, string content, IEnumerable<string> tags, IEnumerable<string> keywords);
    long CreateAnswer(long userId, long questionId, string content, IEnumerable<string> keywords);
    long CreateComment(long userId, CommentTarget target, string content);

    void Vote(long voterId, PostType targetType, long targetId, VoteType voteType);
}
