using StackOverflow.Factories;
using StackOverflow.Models;
using StackOverflow.Repositories;
using StackOverflow.Reputation;

namespace StackOverflow.Services;

public sealed class PostService : IPostService
{
    private readonly IQuestionRepository _questions;
    private readonly IAnswerRepository _answers;
    private readonly ICommentRepository _comments;
    private readonly IUserActivitySubject _activity;

    private readonly IQuestionFactory _questionFactory;
    private readonly IAnswerFactory _answerFactory;
    private readonly ICommentFactory _commentFactory;

    public PostService(
        IQuestionRepository questions,
        IAnswerRepository answers,
        ICommentRepository comments,
        IUserActivitySubject activity,
        IQuestionFactory questionFactory,
        IAnswerFactory answerFactory,
        ICommentFactory commentFactory)
    {
        _questions = questions;
        _answers = answers;
        _comments = comments;
        _activity = activity;

        _questionFactory = questionFactory;
        _answerFactory = answerFactory;
        _commentFactory = commentFactory;
    }

    public long CreateQuestion(long userId, string content, IEnumerable<string> tags, IEnumerable<string> keywords)
    {
        var q = _questionFactory.Create(userId, content, tags, keywords);
        _questions.Save(q);

        _activity.Publish(new UserActivityEvent(userId, ActivityType.PostQuestion, PostType.Question, q.Id, null));
        return q.Id;
    }

    public long CreateAnswer(long userId, long questionId, string content, IEnumerable<string> keywords)
    {
        var a = _answerFactory.Create(userId, questionId, content, keywords);
        _answers.Save(a);

        _activity.Publish(new UserActivityEvent(userId, ActivityType.PostAnswer, PostType.Answer, a.Id, null));
        return a.Id;
    }

    public long CreateComment(long userId, CommentTarget target, string content)
    {
        var c = _commentFactory.Create(userId, target, content);
        _comments.Save(c);

        _activity.Publish(new UserActivityEvent(userId, ActivityType.PostComment, PostType.Comment, c.Id, null));
        return c.Id;
    }

    public void Vote(long voterId, PostType targetType, long targetId, VoteType voteType)
    {
        // Minimal voting: apply score + publish event.
        long? ownerId = targetType switch
        {
            PostType.Question => _questions.GetById(targetId)?.AuthorId,
            PostType.Answer   => _answers.GetById(targetId)?.AuthorId,
            PostType.Comment  => _comments.GetById(targetId)?.AuthorId,
            _ => null
        };

        var delta = (int)voteType;

        switch (targetType)
        {
            case PostType.Question:
                var q = _questions.GetById(targetId);
                if (q is null) return;
                q.ApplyVote(delta);
                _questions.Save(q);
                break;

            case PostType.Answer:
                var a = _answers.GetById(targetId);
                if (a is null) return;
                a.ApplyVote(delta);
                _answers.Save(a);
                break;

            case PostType.Comment:
                var c = _comments.GetById(targetId);
                if (c is null) return;
                c.ApplyVote(delta);
                _comments.Save(c);
                break;
        }

        var activity = voteType == VoteType.Up ? ActivityType.Upvote : ActivityType.Downvote;
        _activity.Publish(new UserActivityEvent(voterId, activity, targetType, targetId, ownerId));
    }
}
