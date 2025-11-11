using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace ProductConfirm.Utilities
{
    public class MemoryCacheService
    {
        private readonly MemoryCache _cache = MemoryCache.Default;

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> loader, int expireMinutes = 10)
        {
            if (_cache.Contains(key)) return (T)_cache.Get(key);


            T value = await loader();
            if (value != null)
            {
                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(expireMinutes)
                };
                _cache.Set(key, value, policy);
            }

            return value;
        }

        public void Remove(string key)
        {
            if (_cache.Contains(key))
                _cache.Remove(key);
        }

        public void ClearAll()
        {
            foreach (var item in _cache)
            {
                _cache.Remove(item.Key);
            }
        }
    }
}
