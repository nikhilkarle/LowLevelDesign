namespace ProxyPattern.Models
{
    public class UserProfile
    {
        public string UserId { get; }
        public string FullName { get; }
        public string Email { get; }

        public UserProfile(string userId, string fullName, string email)
        {
            UserId = userId;
            FullName = fullName;
            Email = email;
        }
    }
}
