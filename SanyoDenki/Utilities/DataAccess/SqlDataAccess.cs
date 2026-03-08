using System.Collections.Concurrent;
using System.Threading;
using System.Data.SqlClient;
//using System.Web.Caching;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Dapper;
using NLog;
using ProgramPartListWeb.Utilities;
using CommandType = System.Data.CommandType;
using System.Configuration;

namespace SanyoDenki.Utilities
{
    public sealed class SqlDataAccess
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // -------------------- CONFIG & CACHE --------------------
        private static readonly string _connectionString = BuildConnectionString();
        private static readonly ObjectCache _cache = MemoryCache.Default;

        // High concurrency locks per cache key
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks
            = new ConcurrentDictionary<string, SemaphoreSlim>();

        private static readonly CacheItemPolicy DefaultPolicy = new CacheItemPolicy
        {
            SlidingExpiration = TimeSpan.FromMinutes(10)
        };



        public static string BuildConnectionString()
        {
            try
            {
                string env = ConfigurationManager.AppSettings["AppEnvironment"];
                string key;

                switch (env)
                {
                    case "Home":
                        key = "HomeDevelopment";
                        break;

                    case "Test":
                        key = "TestDevelopment";
                        break;

                    default:
                        key = "LiveDevelopment";
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
                Logger.Error(ex, "Error connection string. Defaulting to LiveDevelopment");
                return System.Configuration.ConfigurationManager.ConnectionStrings["LiveDevelopment"].ConnectionString;
            } 
        }

        private static SqlConnection CreateConnection()
       => new SqlConnection(_connectionString);


        // --------------------- CORE METHODS ---------------------
        public static async Task<List<T>> GetDataAsync<T>(
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
                            Logger.Error(ex, "Background refresh failed.");
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

        // -------------------- GET OBJECT DATA --------------------
        public static async Task<T> GetSingleAsync<T>(
            string query,
            object parameters = null,
            CommandType commandType = CommandType.Text,
            string cacheKey = null,
            int cacheMinutes = 10)
        {
            if (string.IsNullOrEmpty(cacheKey))
            {
                using (var con = CreateConnection())
                    return await con.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType);
            }

            var cached = _cache.Get(cacheKey);
            if (cached != null)
                return (T)cached;

            var myLock = _locks.GetOrAdd(cacheKey, new SemaphoreSlim(1, 1));

            await myLock.WaitAsync();
            try
            {
                cached = _cache.Get(cacheKey);
                if (cached != null)
                    return (T)cached;

                T result;

                using (var con = CreateConnection())
                    result = await con.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType);

                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheMinutes)
                };

                _cache.Set(cacheKey, result, policy);

                return result;
            }
            finally
            {
                myLock.Release();
            }
        }



        // -------------------- GETS STRING ONLY --------------------
        public static async Task<string> GetOneData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = CreateConnection())
                    return await con.QuerySingleOrDefaultAsync<string>(query, parameters);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
                return "";
            }
        }
        // -------------------- GET THE TOTAL COUNT --------------------
        public static async Task<int> ExecuteScalarAsync(string query,
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
                Logger.Error(ex, $"ExecuteScalarAsync failed. Query: {query}");
                return 0;
            }
        }


        // -------------------- CHECK IF THE DATA EXIST --------------------
        public static async Task<bool> CheckDataAsync(
                string query,
                object parameters = null,
                CommandType commandType = CommandType.Text)
        {
            try
            {
                using (var con = CreateConnection())
                {
                    int count = await con.ExecuteScalarAsync<int>(
                        query,
                        parameters,
                        commandType: commandType
                    );

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"CheckDataAsync failed. Query: {query}");
                return false;
            }

        }
        // -------------------- INSERT AND UPDATE QUERY --------------------
        public static async Task<bool> ExecuteAsync(
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
                Logger.Error(ex, "ExecuteAsync failed.");
                return false;
            }
        }

        // -------------------- GET THE LIST DATA BY STRING --------------------
        public static async Task<List<string>> StringListAsync(
                string query,
                object parameters = null,
                CommandType commandType = CommandType.Text)
        {
            var stringList = new List<string>();

            try
            {
                using (var con = CreateConnection())
                {
                    var result = await con.QueryAsync<string>(
                        query,
                        parameters,
                        commandType: commandType
                    );

                    return result.AsList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"StringListAsync failed. Query: {query}");
                return new List<string>();
            }
        }

        // -------------------- SOFT DELETE --------------------------
        public static async Task<bool> SoftDeleteAsync(
            string tableName,
            string keyColumn,
            object keyValue)
        {
            string sql = $"UPDATE {tableName} SET IsDelete = 1 WHERE {keyColumn} = @Id";

            return await ExecuteAsync(sql, new { Id = keyValue });
        }

        public static async Task BulkInsertAsync<T>(
            string tableName,
            List<T> data)
        {
            if (data == null || !data.Any())
                return;

            using (var con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (var bulkCopy = new SqlBulkCopy(con))
                {
                    bulkCopy.DestinationTableName = tableName;

                    var table = new DataTable();
                    var properties = typeof(T).GetProperties();

                    foreach (var prop in properties)
                    {
                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }

                    foreach (var item in data)
                    {
                        var row = table.NewRow();
                        foreach (var prop in properties)
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        table.Rows.Add(row);
                    }

                    await bulkCopy.WriteToServerAsync(table);
                }
            }
        }

        // -------------------- CACHE UTILITIES --------------------
        public static void ClearCache(string cacheKey = null)
        {
            if (!string.IsNullOrEmpty(cacheKey))
            {
                _cache.Remove(cacheKey);
            }
            else
            {
                foreach (var item in _cache)
                    _cache.Remove(item.Key);
            }
        }


        public static void StartSqlDependency()
        {
            SqlDependency.Start(_connectionString);
        }

        public static void StopSqlDependency()
        {
            SqlDependency.Stop(_connectionString);
        }
    }
}