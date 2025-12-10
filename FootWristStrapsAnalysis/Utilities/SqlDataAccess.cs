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
using ProgramPartListWeb.Utilities;
using ProductConfirm.Utilities;

namespace ProgramPartListWeb.Helper
{
    public static class SqlDataAccess
    {
        private static readonly MemoryCacheService _cache = new MemoryCacheService();
        //private static readonly string _connectionString = AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings["LiveDevelopment"].ConnectionString);

        public static string connectionString()
        {
            try
            {
                string machineName = Environment.MachineName.ToLower();
                string connectionKey = "";

                if (machineName == "desktop-fc0up1p") //  Home production
                    connectionKey = "HomeDevelopment";
                else if(machineName == "sdp04003c")
                    connectionKey = "TestDevelopment";
                else
                    connectionKey = "LiveDevelopment";


                //LogConnectionChoice(machineName, connectionKey);

                return AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings["TestDevelopment"].ConnectionString);
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

        public static SqlConnection GetSqlConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }


        public static async Task<IEnumerable<T>> GetIEnumerableData<T>(string query, object parameters = null)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(connectionString()))
                {
                    // Detect if the query is a stored procedure name (no spaces, only word chars)
                    bool isStoredProc = Regex.IsMatch(query, @"^\w+$");

                    var commandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text;

                    var result = await con.QueryAsync<T>(
                        query,
                        parameters,
                        commandType: commandType
                    );

                    return result; // Already IEnumerable<T>
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
                return Enumerable.Empty<T>();  // safer than null
            }
        }



        // ############ DYNAMIC FUNCTION LIST<T> GETDATA ########################
        public static async Task<List<T>> GetData<T>(string query, object parameters = null)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(connectionString()))
                {
                    var IsStoreProd = Regex.IsMatch(query, @"^\w+$");
                    var commandType = IsStoreProd ? CommandType.StoredProcedure : CommandType.Text;
                    var result = await con.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);

                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Exception: " + ex.Message);
                return null;
            }
            

           // return resultData;
        }
        // ############ SELECTS ONLY ONE ROW DATA  ##############################
        public static async Task<string> GetOneData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(connectionString()))
                {
                    return  await con.QuerySingleOrDefaultAsync<string>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
                return "";
            }
        }
        // ############ GET THE TOTAL COUNT OF THE QUERY ########################
        public static async Task<int> GetCountData(string query, object parameters = null, CommandType commandType = CommandType.Text)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(connectionString()))
                {
                    if (Regex.IsMatch(query, @"^\w+$"))
                    {
                        // This code is a Procudure query
                        int count = await con.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                        return count;
                    }
                    else
                    {
                        // Ordinary Query string
                        int count = await con.ExecuteScalarAsync<int>(query, parameters, commandType: commandType);
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
                return 0;
            }
        }

        // ############ CHECKS IF THE ROW DATA IS EXIST ########################
        public static async Task<bool> Checkdata(string query, object parameters = null)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(connectionString()))
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
                Debug.WriteLine("Error occur : " + ex.Message);
                return false;
            }
           
        }
        // ############ INSERT AND UPDATE QUERY ########################
        public static async Task<bool> UpdateInsertQuery(string strQuery, object parameters)
        {   
            try
            {
                using (IDbConnection con = new SqlConnection(connectionString()))
                {
                    int rowsAffected;

                    if (Regex.IsMatch(strQuery, @"^\w+$"))
                    {
                        // This code is a Procudure query
                        rowsAffected = await con.ExecuteAsync(strQuery, parameters, commandType: CommandType.StoredProcedure);
                    }
                    else
                    {
                        rowsAffected = await con.ExecuteAsync(strQuery, parameters);
                    }
                    return rowsAffected > 0;
                }
            }       
            catch (Exception ex)
            {
                // Log other exceptions
                Debug.WriteLine($"Exception: {ex.Message}");
          
                return false;
            }
            
        }
        public static async Task<List<string>> StringList(string query, object parameters = null)
        {
            var stringList = new List<string>();

            try
            {
                using (IDbConnection con = GetSqlConnection(connectionString()))
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
                using (IDbConnection con = GetSqlConnection(connectionString()))
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
                Debug.WriteLine($"{ex.Message}");
                return 0;
            }
        }

    }
}