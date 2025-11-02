using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Text.RegularExpressions;
using System.Diagnostics;
using ProgramPartListWeb.Utilities;
    

namespace Attendance_Monitoring.Utilities
{
    public sealed class SqlDataAccess
    {
        private static readonly string _cons = ConfigurationManager.ConnectionStrings["live_connect"].ToString();

        public static string _connectionString()
        {
            try
            {
                string machineName = Environment.MachineName.ToLower();
                string connectionKey = "";

                if (machineName == "desktop-fc0up1p") //  Home production
                    connectionKey = "HomeDevelopment";
                else if (machineName == "sdp04003c") //  Test production
                    connectionKey = "TestDevelopment";
                else
                    connectionKey = "LiveDevelopment";


                LogConnectionChoice(machineName, connectionKey);

                return AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings[connectionKey].ConnectionString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "";
            }
        }

        // CHECK CONNECTION 
        private static void LogConnectionChoice(string machineName, string connectionKey)
        {
            string logEntry = $"{DateTime.Now:u} | Machine: {machineName} | Connection: {connectionKey}";
            Debug.WriteLine(logEntry);
        }
        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString());
        }

        public static SqlConnection GetSqlConnection(string connectionString) => new SqlConnection(connectionString);

        // ############ DYNAMIC FUNCTION LIST<T> GETDATA ########################
        public static async Task<List<T>> GetData<T>(string query, object parameters = null)
        {
            //var resultData = new List<T>();
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                  
                    bool IsStoreProd = Regex.IsMatch(query, @"^\w+$");
                    var commandType = IsStoreProd ? CommandType.StoredProcedure : CommandType.Text;
                    var result = await con.QueryAsync<T>(query, parameters, commandType: commandType);

                    return result.ToList();
                    //return resultData;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
                return null;
            }


            // return resultData;
        }


        public static async Task<List<T>> GetDataList<T>(
            string query,
            object parameters = null,
            string cacheKey = null,
            int cacheMinutes = 10)
        {
            try
            {
                bool IsStoredProcedure = Regex.IsMatch(query, @"^\w+$", RegexOptions.IgnoreCase);

                async Task<List<T>> FetchData()
                {
                    using (IDbConnection con = SqlDataAccess.CreateConnection())
                    {
                        var commandType = IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
                        var result = await con.QueryAsync<T>(query, parameters, commandType: commandType);
                        return result.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(cacheKey))
                {
                    return await CacheHelper.GetOrSetAsync(cacheKey, FetchData, cacheMinutes);
                }


                return await FetchData();
            }
            catch (SqlException ex)
            {
                //_logger.Error(ex, $"SQL Exception in {nameof(GetDataList)} | Query: {query} | CacheKey: {cacheKey}");
                return null;
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, $"Unexpected error in {nameof(GetDataList)} | Query: {query}");
                return null;
            }
        }


        // ############ SELECTS ONLY ONE ROW DATA  ##############################
        public static async Task<T> GetOneData<T>(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    return await con.QuerySingleOrDefaultAsync<T>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: {ex.Message}");
            }
        }

        // ############ GET THE TOTAL COUNT OF THE QUERY ########################
        public static async Task<int> GetCountData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    int count = 0;

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
                Debug.Write(ex.Message);    
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
                    bool IsStoreprod = Regex.IsMatch(query, @"^\w+$");
                    var commandType = IsStoreprod ? CommandType.StoredProcedure : CommandType.Text;

                    int count = await con.ExecuteScalarAsync<int>(query, parameters, commandType: commandType);
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        // ############ INSERT AND UPDATE QUERY ########################
        public static async Task<bool> UpdateInsertQuery(string strQuery, object parameters)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {

                    bool isStoredProcedure = Regex.IsMatch(strQuery, @"^\w+$");
                    CommandType commandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

                    int rowsAffected = await con.ExecuteAsync(strQuery, parameters, commandType: commandType);

                    // If update was successful and a cache key is provided, remove it
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");

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
                Debug.WriteLine("SQL Exception: " + ex.Message);
                return 0;
            }
        }


        // ############ GET DATA BY DATATABLE ##################
        public static async Task<DataTable> GetDataByDataTable(string query, object parameters = null)
        {
            var resultData = new DataTable();
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    // Execute the query using Dapper
                    var result = await con.QueryAsync(query, parameters);

                    // If there are results, populate the DataTable
                    if (result != null && result.Any())
                    {
                        // Create columns dynamically based on the first record
                        var firstRow = (IDictionary<string, object>)result.First();
                        foreach (var column in firstRow)
                        {
                            resultData.Columns.Add(column.Key, column.Value?.GetType() ?? typeof(string));
                        }

                        // Add rows
                        foreach (var record in result)
                        {
                            var row = resultData.NewRow();
                            var dict = (IDictionary<string, object>)record;
                            foreach (var column in dict)
                            {
                                row[column.Key] = column.Value ?? DBNull.Value;
                            }
                            resultData.Rows.Add(row);
                        }
                    }

                    return resultData;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SQL Exception: " + ex.Message);
            }

            return resultData;
        }
    }
}
