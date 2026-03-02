using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Dapper;
using Microsoft.AspNet.SignalR.Messaging;
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


        public static async Task<T> GetObjectOnly<T>(string query, object parameters = null, string cacheKey = null, int cacheMinutes = 10)
        {
            try
            {
                // Use cache only if key is provided
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    return await CacheHelper.GetOrSetAsync(cacheKey, async () =>
                    {
                        using (IDbConnection con = CreateConnection())
                        {
                            bool isStoredProc = Regex.IsMatch(query, @"^\w+$");
                            var commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                            // Get the first record only
                            return await con.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType);
                        }
                    }, cacheMinutes);
                }
                else
                {
                    using (IDbConnection con = CreateConnection())
                    {
                        bool isStoredProc = Regex.IsMatch(query, @"^\w+$");
                        var commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                        return await con.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: commandType);
                    }
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex, $"SQL Exception while executing single-object query. Query: {query}, CacheKey: {cacheKey}");
                return default(T); // null for classes, zero/default for structs
            }
        }



        // ############ SELECTS ONLY ONE ROW DATA  ##############################
        public static async Task<string> GetOneData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = CreateConnection())
                {
                    return  await con.QuerySingleOrDefaultAsync<string>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
                return "";
            }
        }
        // ############ GET THE TOTAL COUNT OF THE QUERY ########################
        public static async Task<int> GetCountData(string query, object parameters = null)
        {
            try
            {
                using (IDbConnection con = CreateConnection())
                {
                    // Check if the query is a stored procedure name (only word characters, no spaces/symbols)
                    var isStoredProcedure = Regex.IsMatch(query, @"^\w+$");

                    int count = await con.ExecuteScalarAsync<int>(
                        query,
                        parameters,
                        commandType: isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text
                    );

                    return count;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
                return 0;
            }
        }

        // ############ CHECKS IF THE ROW DATA IS EXIST ########################
        public static async Task<bool> Checkdata(string query, object parameters = null)
        {
            try
            {
                using (IDbConnection con = CreateConnection())
                {
                    bool IsStoreprod = Regex.IsMatch(query, @"^\w+$");
                    var commandType = IsStoreprod ? CommandType.StoredProcedure : CommandType.Text;

                    int count = await con.ExecuteScalarAsync<int>(query, parameters, commandType: commandType);
                    return count > 0;
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
                return false;
            }
           
        }
        // ############ INSERT AND UPDATE QUERY ########################
        public static async Task<bool> ExecuteAsync(
            string query,
            object parameters = null,
            CommandType commandType = CommandType.Text,
            string cacheKeyToInvalidate = null)
        {
            try { 
                using (var con = CreateConnection()) 
                { 
                    int rowsAffected = 
                        await con.ExecuteAsync(query, parameters, commandType: commandType); 
                    if (rowsAffected > 0 && !string.IsNullOrEmpty(cacheKeyToInvalidate))
                        _cache.Remove(cacheKeyToInvalidate); return rowsAffected > 0; } } 
            catch (Exception ex) 
            { 
                Logger.Error(ex, $"ExecuteAsync failed. Query: {query}"); 
                return false; 
            }
        }


        public static async Task<List<string>> StringList(string query, object parameters = null)
        {
            var stringList = new List<string>();

            try
            {
                using (IDbConnection con = CreateConnection())
                {
                    IEnumerable<string> dataList;

                    bool IsStoreProd = Regex.IsMatch(query, @"^\w+$");
                    var commandType = IsStoreProd ? CommandType.StoredProcedure : CommandType.Text;
                    dataList = await con.QueryAsync<string>(query, parameters, commandType: commandType);

                    stringList = dataList.ToList();
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
            }

            return stringList;
        }
        public static async Task<int> GetCountDataSync(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = CreateConnection())
                {
                    int count;
                    bool IsStoreProd = Regex.IsMatch(query, @"^\w+$");
                    var commandType = IsStoreProd ? CommandType.StoredProcedure : CommandType.Text;
                    count = await con.ExecuteScalarAsync<int>(query, parameters, commandType: commandType);
                    return count;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
                return 0;
            }
        }




       
    }
}