namespace StackOverflow.Models;

public enum PostType {Question, Answer, Comment}
public enum VoteType {Up = 1, Down = -1}
public sealed record CommentTarget(PostType Type, long TargetId);