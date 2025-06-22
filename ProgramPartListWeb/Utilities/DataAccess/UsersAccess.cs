using Dapper;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Web;
using ProgramPartListWeb.Utilities;


namespace ProgramPartListWeb.Utilities
{
    public sealed class UsersAccess
    {
        // Auto Connection Based on the Domain URL
        public static string _connectionString()
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            string machineName = Environment.MachineName.ToLower();
            string connectionKey = "";

            if (host.Contains("p1saportalweb.sdp.com"))
            {
                connectionKey = "UsersLiveConnection";
            }

            if (host.Contains("localhost"))
            {
                if (machineName == "desktop-fc0up1p") // Home PC name
                    connectionKey = "HomeDevelopment";
                else
                    connectionKey = "UsersTestConnection";
            }


            LogConnectionChoice(host, machineName, connectionKey);

            return AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings[connectionKey].ConnectionString);
        }

        private static void LogConnectionChoice(string host, string machineName, string connectionKey)
        {
            string logEntry = $"{DateTime.Now:u} | Host: {host} | Machine: {machineName} | Connection: {connectionKey}";
            Debug.WriteLine(logEntry);
        }

        public static SqlConnection GetSqlConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        // #################################### USER MANAGEMENT ===================================

        public static async Task<List<T>> UserGetData<T>(string query, object parameters = null, string cacheKey = null, int cacheMinutes = 10)
        {
            try
            {
                // Use provided cache key or default to no caching
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    return await CacheHelper.GetOrSetAsync(cacheKey, async () =>
                    {
                        using (IDbConnection con = GetSqlConnection(_connectionString()))
                        {
                            if (Regex.IsMatch(query, @"^\w+$"))
                            {
                                return (await con.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure)).ToList();
                            }
                            else
                            {
                                return (await con.QueryAsync<T>(query, parameters)).ToList();
                            }
                        }
                    }, cacheMinutes);
                }
                else
                {
                    // No caching
                    using (IDbConnection con = GetSqlConnection(_connectionString()))
                    {
                        if (Regex.IsMatch(query, @"^\w+$"))
                        {
                            return (await con.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure)).ToList();
                        }
                        else
                        {
                            return (await con.QueryAsync<T>(query, parameters)).ToList();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                CustomLogger.LogError(ex);
                return null;
            }
        }
        public static async Task<bool> UpdateUserData(string strQuery, object parameters, string cacheKeyToInvalidate = null)
        {
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString()))
                {
                    int rowsAffected;

                    bool isStoredProcedure = Regex.IsMatch(strQuery, @"^\w+$");
                    CommandType commandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

                    rowsAffected = await con.ExecuteAsync(strQuery, parameters, commandType: commandType);

                    // If update was successful and a cache key is provided, remove it
                    if (rowsAffected > 0 && !string.IsNullOrEmpty(cacheKeyToInvalidate))
                    {
                        CacheHelper.Remove(cacheKeyToInvalidate);
                    }

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                CustomLogger.LogError(ex);
                Debug.WriteLine($"Exception in UpdateInsertQuery: {ex.Message}");
                return false;
            }
        }
        public static async Task<int> GetUserCountData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    int count = await con.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                    return count;
                }
            }
            catch (Exception ex)
            {
                CustomLogger.LogError(ex);
                return 0;
            }
        }
    }
}