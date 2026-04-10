using Facebook.Domain.Enums;

namespace Facebook.Domain.Entities;

public class User
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Email { get; }
    public string PasswordHash { get; private set; }
    public string Bio { get; private set; }
    public string ProfilePictureUrl { get; private set; }
    public List<string> Interests { get; private set; }
    public PrivacySetting ProfilePrivacy { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; }

    // Friends are stored as IDs — the FriendService owns the relationship
    private readonly HashSet<Guid> _friendIds = new();
    private readonly HashSet<Guid> _blockedUserIds = new();

    public IReadOnlySet<Guid> FriendIds => _friendIds;
    public IReadOnlySet<Guid> BlockedUserIds => _blockedUserIds;

    public User(Guid id, string name, string email, string passwordHash)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Bio = string.Empty;
        ProfilePictureUrl = string.Empty;
        Interests = new List<string>();
        ProfilePrivacy = PrivacySetting.Public;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(string name, string bio, string profilePictureUrl, List<string> interests)
    {
        Name = name;
        Bio = bio;
        ProfilePictureUrl = profilePictureUrl;
        Interests = interests;
    }

    public void SetProfilePrivacy(PrivacySetting privacy) => ProfilePrivacy = privacy;

    public void AddFriend(Guid friendId) => _friendIds.Add(friendId);

    public void RemoveFriend(Guid friendId) => _friendIds.Remove(friendId);

    public void BlockUser(Guid userId)
    {
        _blockedUserIds.Add(userId);
        _friendIds.Remove(userId); // blocked users are automatically unfriended
    }

    public bool IsFriendWith(Guid userId) => _friendIds.Contains(userId);

    public bool HasBlocked(Guid userId) => _blockedUserIds.Contains(userId);
}
