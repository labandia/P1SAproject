using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using ProgramPartListWeb.Utilities;
using CommandType = System.Data.CommandType;

namespace ProgramPartListWeb.Helper
{
    public sealed class SqlDataAccess
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // -------------------- CONFIG & CACHE --------------------
        private static readonly string _connectionString = BuildConnectionString();
        private static readonly MemoryCache _cache =
         new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 });

        public static string BuildConnectionString()
        {
            try
            {
                string connectionKey = "LiveDevelopment";

                if (System.Web.HttpContext.Current != null)
                {
                    string host = System.Web.HttpContext.Current.Request.Url.Host.ToLower();
                    string hostname = Environment.MachineName.ToLower();

                    if (host.Contains("p1saportalweb.sdp.com"))
                        connectionKey = "LiveDevelopment";
                    else if (host.Contains("localhost"))
                        connectionKey = hostname == "desktop-fc0up1p" ? "HomeDevelopment" : "TestDevelopment";
                }

                // AES decode happens once at app start
                return AesEncryption.DecodeBase64ToString(
                    System.Configuration.ConfigurationManager.ConnectionStrings[connectionKey].ConnectionString
                );
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error connection string. Defaulting to LiveDevelopment");
                return System.Configuration.ConfigurationManager.ConnectionStrings["LiveDevelopment"].ConnectionString;
            } 
        }

        private static SqlConnection CreateConnection()
       => new SqlConnection(_connectionString);


        // ############ DYNAMIC FUNCTION LIST<T> GETDATA ########################
        public static async Task<List<T>> GetDataAsync<T>(
           string query,
           object parameters = null,
           CommandType commandType = CommandType.Text,
           string cacheKey = null,
           int cacheMinutes = 10)
        {
            if (!string.IsNullOrEmpty(cacheKey) &&
                _cache.TryGetValue(cacheKey, out List<T> cached))
            {
                return cached;
            }

            try
            {
                using (var con = CreateConnection())
                {
                    var result = await con.QueryAsync<T>(query, parameters, commandType: commandType);
                    var list = result.AsList();

                    if (!string.IsNullOrEmpty(cacheKey))
                    {
                        _cache.Set(cacheKey, list,
                            new MemoryCacheEntryOptions
                            {
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheMinutes),
                                Size = 1
                            });
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "GetDataAsync failed.");
                return new List<T>();
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
            if (!string.IsNullOrEmpty(cacheKey))
            {
                if (_cache.TryGetValue(cacheKey, out T cached))
                    return cached;
            }

            try
            {
                using (var con = CreateConnection())
                {
                    var result = await con.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType);

                    if (!string.IsNullOrEmpty(cacheKey))
                    {
                        _cache.Set(cacheKey, result, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheMinutes),
                            Size = 1
                        });
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"GetSingleAsync failed. Query: {query}, CacheKey: {cacheKey}");
                return default;
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
                    int rowsAffected = await con.ExecuteAsync(query, parameters, commandType: commandType);

                    if (rowsAffected > 0 && !string.IsNullOrEmpty(cacheKeyToInvalidate))
                        _cache.Remove(cacheKeyToInvalidate);

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"ExecuteAsync failed. Query: {query}");
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
                _cache.Remove(cacheKey);
            else
                _cache.Dispose();
        }
    }
}