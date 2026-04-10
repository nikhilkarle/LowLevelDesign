using Facebook.Application.Factories;
using Facebook.Application.Observers;
using Facebook.Application.Services;
using Facebook.Application.Strategies;
using Facebook.Domain.Enums;
using Facebook.Domain.Rules;
using Facebook.Infrastructure.Repositories;

var userRepo         = new InMemoryUserRepository();
var postRepo         = new InMemoryPostRepository();
var friendRequestRepo = new InMemoryFriendRequestRepository();
var notificationRepo = new InMemoryNotificationRepository();

var notificationService = NotificationService.GetInstance(notificationRepo);

var visibilityStrategies = new Dictionary<PrivacySetting, IVisibilityStrategy>
{
    { PrivacySetting.Public,      new PublicVisibilityStrategy() },
    { PrivacySetting.FriendsOnly, new FriendsOnlyVisibilityStrategy() },
    { PrivacySetting.Private,     new PrivateVisibilityStrategy() }
};

var friendRequestRules = new CompositeFriendRequestRule(new IFriendRequestRule[]
{
    new SelfRequestRule(),
    new AlreadyFriendsRule(),
    new BlockedUserRule()
});

var userService   = new UserService(userRepo);
var friendService = new FriendService(userRepo, friendRequestRepo, notificationService, friendRequestRules);
var postService   = new PostService(postRepo, userRepo, new PostFactory(), notificationService, visibilityStrategies);
var newsfeedService = new NewsfeedService(postRepo, userRepo, visibilityStrategies, new ChronologicalNewsfeedStrategy());


Console.WriteLine("=== Register Users ===");
var alice = userService.Register("Alice", "alice@example.com", "hash_alice");
var bob   = userService.Register("Bob",   "bob@example.com",   "hash_bob");
var carol = userService.Register("Carol", "carol@example.com", "hash_carol");
Console.WriteLine($"Registered: {alice.Name}, {bob.Name}, {carol.Name}");

// Subscribe to real-time notifications (Observer Pattern)
var aliceObserver = new UserNotificationObserver(alice.Id);
var bobObserver   = new UserNotificationObserver(bob.Id);
notificationService.Subscribe(alice.Id, aliceObserver);
notificationService.Subscribe(bob.Id, bobObserver);

Console.WriteLine("\n=== Friend Requests ===");
var req = friendService.SendRequest(alice.Id, bob.Id);  
Console.WriteLine($"Alice sent friend request to Bob (id: {req.Id})");

friendService.AcceptRequest(req.Id, bob.Id);        
Console.WriteLine("Bob accepted Alice's friend request");

Console.WriteLine("\n=== Update Profile ===");
userService.UpdateProfile(alice.Id, "Alice Smith", "Software engineer", "https://cdn.example.com/alice.jpg", new() { "coding", "hiking" });
Console.WriteLine($"Alice updated her profile");

Console.WriteLine("\n=== Create Posts ===");
var post1 = postService.CreatePost(alice.Id, PostType.Text, "Hello world! My first post.", PrivacySetting.Public);
var post2 = postService.CreatePost(bob.Id, PostType.Text, "Good morning everyone!", PrivacySetting.FriendsOnly);
var post3 = postService.CreatePost(carol.Id, PostType.Text, "Carol's private note.", PrivacySetting.Private);
Console.WriteLine($"Created posts: {post1.Id}, {post2.Id}, {post3.Id}");

Console.WriteLine("\n=== Newsfeed for Alice (chronological) ===");
var feed = newsfeedService.GetNewsfeed(alice.Id);
foreach (var post in feed)
    Console.WriteLine($"  [{post.CreatedAt:HH:mm:ss}] {post.AuthorId}: \"{post.TextContent}\" ({post.Privacy})");

Console.WriteLine("\n=== Newsfeed for Alice (by popularity) ===");
newsfeedService.SetSortStrategy(new PopularityNewsfeedStrategy());
var feedPopular = newsfeedService.GetNewsfeed(alice.Id);
foreach (var post in feedPopular)
    Console.WriteLine($"  Likes={post.LikedByUserIds.Count} Comments={post.Comments.Count} | \"{post.TextContent}\"");

Console.WriteLine("\n=== Likes and Comments ===");
postService.LikePost(bob.Id, post1.Id);       
postService.AddComment(bob.Id, post1.Id, "Great post, Alice!");  

Console.WriteLine("\n=== Alice's Notifications ===");
foreach (var n in notificationService.GetNotificationsForUser(alice.Id))
    Console.WriteLine($"  [{n.Type}] {n.Message} (read={n.IsRead})");

Console.WriteLine("\n=== Privacy Enforcement ===");
try
{
    postService.LikePost(carol.Id, post2.Id); 
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine($"  Access denied for Carol on Bob's post: {ex.Message}");
}

Console.WriteLine("\n=== Chain of Responsibility: Duplicate Friend Request ===");
try
{
    friendService.SendRequest(alice.Id, bob.Id);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  Blocked: {ex.Message}");
}
