using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProgramPartListWeb.Utilities;
using System.Configuration;
using Dapper;

namespace ProductConfirm.Utilities
{
    public sealed class UsersAccess
    {
        private static readonly string _connectionString = AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings["UsersLiveConnection"].ConnectionString);

        // Auto Connection Based on the Domain URL
        public static SqlConnection GetSqlConnection(string connectionString) => new SqlConnection(connectionString);
      

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
                        using (IDbConnection con = GetSqlConnection(_connectionString))
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
                    using (IDbConnection con = GetSqlConnection(_connectionString))
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
                using (IDbConnection con = new SqlConnection(_connectionString))
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
                using (IDbConnection con = GetSqlConnection(_connectionString))
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
