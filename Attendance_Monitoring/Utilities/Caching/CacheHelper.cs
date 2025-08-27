using System;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Utilities
{
    public static class CacheHelper
    {
        private static readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public static async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> fetchFunc, int cacheMinutes = 10)
        {
            if (_cache.TryGetValue(key, out T cachedValue))
            {
                return cachedValue;
            }

            var value = await fetchFunc();

            var options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheMinutes));

            _cache.Set(key, value, options);

            return value;
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

    }
}