using StackOverflow.Factories;
using StackOverflow.Models;
using StackOverflow.Repositories;
using StackOverflow.Reputation;
using StackOverflow.Search;
using StackOverflow.Services;

IUserRepository users = new InMemoryUserRepository();
IQuestionRepository questions = new InMemoryQuestionRepository();
IAnswerRepository answers = new InMemoryAnswerRepository();
ICommentRepository comments = new InMemoryCommentRepository();

users.Save(new User { Id = 1, Name = "Alice" });
users.Save(new User { Id = 2, Name = "Bob" });

IUserActivitySubject activitySubject = new UserActivitySubject();
IReputationCalculator repCalc = new ReputationCalculator(users);
activitySubject.Subscribe(repCalc);

IQuestionFactory questionFactory = new QuestionFactory();
IAnswerFactory answerFactory = new AnswerFactory();
ICommentFactory commentFactory = new CommentFactory();

IPostService postService = new PostService(
    questions,
    answers,
    comments,
    activitySubject,
    questionFactory,
    answerFactory,
    commentFactory
);

ISearchStrategy keywordStrategy = new KeywordSearchStrategy(questions);
ISearchStrategy tagStrategy = new TagSearchStrategy(questions);
ISearchStrategy userStrategy = new UserProfileSearchStrategy(questions);

ISearchService searchService = new SearchService(keywordStrategy, tagStrategy, userStrategy);

var qId = postService.CreateQuestion(
    userId: 1,
    content: "  How do I implement Observer pattern in C#?  ",
    tags: new[] { "csharp", "design-patterns", "CSharp" },
    keywords: new[] { "observer", "events", "Observer" }
);

var aId = postService.CreateAnswer(
    userId: 2,
    questionId: qId,
    content: " Use an interface for observers and a subject that publishes events. ",
    keywords: new[] { "observer", "subject" }
);

var cId = postService.CreateComment(
    userId: 1,
    target: new CommentTarget(PostType.Answer, aId),
    content: "Nice. Can you share a small example?"
);

postService.Vote(voterId: 2, targetType: PostType.Question, targetId: qId, voteType: VoteType.Up);
postService.Vote(voterId: 1, targetType: PostType.Answer, targetId: aId, voteType: VoteType.Up);
postService.Vote(voterId: 2, targetType: PostType.Comment, targetId: cId, voteType: VoteType.Down);

Console.WriteLine("Reputation:");
foreach (var u in users.GetAll().OrderBy(u => u.Id))
    Console.WriteLine($"User {u.Id} ({u.Name}) => {u.ReputationScore}");

Console.WriteLine("\nSearch by tag = design-patterns");
foreach (var q in searchService.Search(new SearchQuery(null, "design-patterns", null)))
    Console.WriteLine($"Q{q.Id}: {q.Content} (votes={q.VoteScore})");

Console.WriteLine("\nSearch by keyword = observer");
foreach (var q in searchService.Search(new SearchQuery("observer", null, null)))
    Console.WriteLine($"Q{q.Id}: {q.Content} (votes={q.VoteScore})");

Console.WriteLine("\nSearch by userId = 1 (Alice)");
foreach (var q in searchService.Search(new SearchQuery(null, null, 1)))
    Console.WriteLine($"Q{q.Id}: {q.Content} (votes={q.VoteScore})");
