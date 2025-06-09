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

namespace ProgramPartListWeb.Helper
{
    public static class SqlDataAccess
    {
        private static readonly string _connectionString = AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings["LiveDevelopment"].ConnectionString);

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
        public static async Task<List<T>> GetData<T>(string query, object parameters = null)
        {
            //var resultData = new List<T>();
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString))
                {
                    // Checks if the string is one word
                    if(Regex.IsMatch(query, @"^\w+$"))
                    {
                        // This code is a Procudure query
                        return (await con.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure)).ToList();
                    }
                    else
                    {
                        // Ordinary Query string
                        return (await con.QueryAsync<T>(query, parameters)).ToList();
                    }
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
        // ############ SELECTS ONLY ONE ROW DATA  ##############################
        public static async Task<string> GetOneData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString))
                {
                    return  await con.QuerySingleOrDefaultAsync<string>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        // ############ GET THE TOTAL COUNT OF THE QUERY ########################
        public static async Task<int> GetCountData(string query, object parameters)
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
                return 0;
            }  
        }

        // ############ CHECKS IF THE ROW DATA IS EXIST ########################
        public static async Task<bool> Checkdata(string query, object parameters = null)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString))
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
                return false;
            }
           
        }
        // ############ INSERT AND UPDATE QUERY ########################
        public static async Task<bool> UpdateInsertQuery(string strQuery, object parameters)
        {   
            try
            {
                using (IDbConnection con = new SqlConnection(_connectionString))
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
                using (IDbConnection con = GetSqlConnection(_connectionString))
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
                using (IDbConnection con = GetSqlConnection(_connectionString))
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
                return 0;
            }
        }

    }
}