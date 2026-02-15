using System;
using System.Threading;
using ProxyPattern.Models;

namespace ProxyPattern.Services
{
    public class RealUserProfileService : IUserProfileService
    {
        public UserProfile GetProfile(string userId)
        {
            Thread.Sleep(800);

            return new UserProfile(
                userId: userId,
                fullName: "User " + userId,
                email: userId + "@example.com"
            );
        }
    }
}
