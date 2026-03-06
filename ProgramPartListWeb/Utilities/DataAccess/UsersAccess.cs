using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Threading;
using System.Collections.Concurrent;
using CommandType = System.Data.CommandType;


namespace ProgramPartListWeb.Utilities
{
    public sealed class UsersAccess
    {
        // -------------------- CONFIG & CACHE --------------------
        private static readonly string _connectionString = BuildConnectionString();
        private static readonly ObjectCache _cache = MemoryCache.Default;


        // High concurrency locks per cache key
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks
            = new ConcurrentDictionary<string, SemaphoreSlim>();

        public static string BuildConnectionString()
        {
            try
            {
                string env = ConfigurationManager.AppSettings["AppEnvironment"];
                string key;

                switch (env)
                {
                    case "Home":
                        key = "UsersHomeConnection";
                        break;

                    case "Test":
                        key = "UsersTestConnection";
                        break;

                    default:
                        key = "UsersLiveConnection";
                        break;
                }

                var encrypted = ConfigurationManager
                    .ConnectionStrings[key]
                    .ConnectionString;

                // AES decode happens once at app start
                return AesEncryption.DecodeBase64ToString(encrypted);
            }
            catch (Exception ex)
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["LiveDevelopment"].ConnectionString;
            }
        }

   

        private static SqlConnection CreateConnection()
       => new SqlConnection(_connectionString);

        // #################################### USER MANAGEMENT ===================================

        public static async Task<List<T>> UserGetData<T>(
          string query,
          object parameters = null,
          CommandType commandType = CommandType.Text,
          string cacheKey = null,
          int cacheMinutes = 10,
          bool useSqlDependency = false)
        {
            if (string.IsNullOrEmpty(cacheKey))
            {
                using (var con = CreateConnection())
                {
                    var result = await con.QueryAsync<T>(query, parameters, commandType: commandType);
                    return result.AsList();
                }
            }

            // First check (no lock)
            var cached = _cache.Get(cacheKey) as List<T>;
            if (cached != null)
                return cached;

            var myLock = _locks.GetOrAdd(cacheKey, new SemaphoreSlim(1, 1));

            await myLock.WaitAsync();
            try
            {
                // Double check
                cached = _cache.Get(cacheKey) as List<T>;
                if (cached != null)
                    return cached;

                List<T> list;

                using (var con = CreateConnection())
                {
                    var result = await con.QueryAsync<T>(query, parameters, commandType: commandType);
                    list = result.AsList();
                }

                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheMinutes)
                };

                // 🔥 SQL AUTO INVALIDATE
                if (useSqlDependency)
                {
                    using (var connection = new SqlConnection(_connectionString))
                    using (var command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        var dependency = new SqlDependency(command);

                        policy.ChangeMonitors.Add(
                            new SqlChangeMonitor(dependency)
                        );
                    }
                }

                // 🔥 BACKGROUND AUTO REFRESH
                policy.RemovedCallback = async args =>
                {
                    if (args.RemovedReason == CacheEntryRemovedReason.Expired)
                    {
                        try
                        {
                            using (var con = CreateConnection())
                            {
                                var result = await con.QueryAsync<T>(query, parameters, commandType: commandType);
                                var refreshed = result.AsList();

                                _cache.Set(cacheKey, refreshed,
                                    DateTimeOffset.Now.AddMinutes(cacheMinutes));
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex, "Background refresh failed.");
                        }
                    }
                };

                _cache.Set(cacheKey, list, policy);

                return list;
            }
            finally
            {
                myLock.Release();
            }
        }
        public static async Task<bool> UpdateUserData(
            string query,
            object parameters = null,
            CommandType commandType = CommandType.Text,
            string cacheKeyToInvalidate = null)
        {
            try
            {
                using (var con = CreateConnection())
                {
                    int rows = await con.ExecuteAsync(query, parameters, commandType: commandType);

                    if (rows > 0 && !string.IsNullOrEmpty(cacheKeyToInvalidate))
                        _cache.Remove(cacheKeyToInvalidate);

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "ExecuteAsync failed.");
                return false;
            }
        }
        public static async Task<int> GetUserCountData(string query,
            object parameters = null,
            CommandType commandType = CommandType.Text)
        {
            try
            {
                using (var con = CreateConnection())
                    return await con.ExecuteScalarAsync<int>(query, parameters, commandType: commandType);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, $"ExecuteScalarAsync failed. Query: {query}");
                return 0;
            }
        }
    }
}