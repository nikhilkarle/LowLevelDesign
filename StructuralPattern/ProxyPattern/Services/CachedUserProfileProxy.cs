using System;
using System.Collections.Generic;
using ProxyPattern.Models;

namespace ProxyPattern.Services
{
    public class CachedUserProfileProxy : IUserProfileService
    {
        private readonly Dictionary<string, UserProfile> _cache;

        private RealUserProfileService _realService;

        public CachedUserProfileProxy()
        {
            _cache = new Dictionary<string, UserProfile>(StringComparer.OrdinalIgnoreCase);
            _realService = null;
        }

        public UserProfile GetProfile(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            UserProfile cached;
            bool found = _cache.TryGetValue(userId, out cached);

            if (found)
            {
                Console.WriteLine("[PROXY] Cache hit for " + userId);
                return cached;
            }

            Console.WriteLine("[PROXY] Cache miss for " + userId);

            if (_realService == null)
            {
                Console.WriteLine("[PROXY] Creating RealUserProfileService (lazy init)");
                _realService = new RealUserProfileService();
            }

            UserProfile profile = _realService.GetProfile(userId);
            _cache[userId] = profile;

            return profile;
        }
    }
}
