﻿using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Utilities
{
    public static class CacheHelper
    {
        private static readonly ObjectCache _cache = MemoryCache.Default;
        //public static T GetOrSet<T>(string key, Func<T> getData, int expirationMinutes = 10)
        //{
        //    if (_cache.Contains(key))
        //    {
        //        return (T)_cache[key];
        //    }

        //    T data = getData();
        //    if (data != null)
        //    {
        //        _cache.Set(key, data, DateTimeOffset.Now.AddMinutes(expirationMinutes));
        //    }

        //    return data;
        //}

        public static async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> getDataAsync, int expirationMinutes = 10)
        {
            if (_cache.Contains(key))
            {
                return (T)_cache[key];
            }

            T data = await getDataAsync();

            if (data != null)
            {
                _cache.Set(key, data, DateTimeOffset.Now.AddMinutes(expirationMinutes));
            }

            return data;
        }

        public static void Remove(string key)
        {
            if (_cache.Contains(key))
            {
                _cache.Remove(key);
            }
        }
    }
}