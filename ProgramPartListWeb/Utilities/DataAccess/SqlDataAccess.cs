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

namespace ProgramPartListWeb.Helper
{
    public sealed class SqlDataAccess
    {
        // Auto Connection Based on the Domain URL
        public static string _connectionString()
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            string machineName = Environment.MachineName.ToLower();
            string connectionKey = "";

            if (host.Contains("p1saportalweb.sdp.com"))
            {
                connectionKey = "LiveDevelopment";
            }

            if (host.Contains("localhost"))
            {
                if (machineName == "desktop-fc0up1p") // Home PC name
                    connectionKey = "HomeDevelopment";
                else
                    connectionKey = "TestDevelopment";
            }


            LogConnectionChoice(host, machineName, connectionKey);

            return AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings[connectionKey].ConnectionString);
        }



        // CHECK CONNECTION 
        private static void LogConnectionChoice(string host, string machineName, string connectionKey)
        {
            string logEntry = $"{DateTime.Now:u} | Host: {host} | Machine: {machineName} | Connection: {connectionKey}";
            Debug.WriteLine(logEntry);
        }

        public static SqlConnection GetSqlConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }


        // ############ DYNAMIC FUNCTION LIST<T> GETDATA ########################
        public static async Task<List<T>> GetData<T>(string query, object parameters = null, string cacheKey = null, int cacheMinutes = 10)
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

        // ############ SELECTS ONLY ONE ROW DATA  ##############################
        public static async Task<string> GetOneData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    return  await con.QuerySingleOrDefaultAsync<string>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                CustomLogger.LogError(ex);
                return "";
            }
        }
        // ############ GET THE TOTAL COUNT OF THE QUERY ########################
        public static async Task<int> GetCountData(string query, object parameters)
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

        // ############ CHECKS IF THE ROW DATA IS EXIST ########################
        public static async Task<bool> Checkdata(string query, object parameters = null)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    int count;
                    // Checks if the string is one word
                    if (Regex.IsMatch(query, @"^\w+$"))
                    {
                        // This code is a Procudure query
                        count = await con.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        count = await con.ExecuteScalarAsync<int>(query, parameters);
                    }
                    return count > 0;
                }
            }
            catch(Exception ex)
            {
                CustomLogger.LogError(ex);
                return false;
            }
           
        }
        // ############ INSERT AND UPDATE QUERY ########################
        public static async Task<bool> UpdateInsertQuery(string strQuery, object parameters, string cacheKeyToInvalidate = null)
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


        public static async Task<List<string>> StringList(string query, object parameters = null)
        {
            var stringList = new List<string>();

            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    IEnumerable<string> dataList;

                    // Check if the query is a stored procedure name (no spaces or symbols, just word characters)
                    if (Regex.IsMatch(query, @"^\w+$"))
                    {
                        dataList = await con.QueryAsync<string>(query, parameters, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        dataList = await con.QueryAsync<string>(query, parameters);
                    }

                    stringList = dataList.ToList();
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine("SQL Exception: " + ex.Message);
            }

            return stringList;
        }
        public static async Task<int> GetCountDataSync(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    int count;
                    if (Regex.IsMatch(query, @"^\w+$"))
                    {
                        // This code is a Procudure query
                        count = await con.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        count = await con.ExecuteScalarAsync<int>(query, parameters);
                    }
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