using ProxyPattern.Models;

namespace ProxyPattern.Services
{
    public interface IUserProfileService
    {
        UserProfile GetProfile(string userId);
    }
}
