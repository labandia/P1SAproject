﻿using Dapper;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Parts_locator.Utilities
{
    public static class SqlDataAccess
    {
        private static readonly string _cons = ConfigurationManager.ConnectionStrings["LiveDevelopment"].ToString();
        //private static readonly string _consV2 = AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings["LiveDevelopment"].ConnectionString);
        //private static readonly string _cons = ConfigurationManager.ConnectionStrings["Myconnect"].ToString();
        public static SqlConnection GetConnection(string connectionString) =>  new SqlConnection(connectionString);
    
        // ############ DYNAMIC FUNCTION LIST<T> GETDATA ########################
        public static async Task<List<T>> GetData<T>(string query, object parameters = null)
        {
            //var resultData = new List<T>();
            try
            {
                using (IDbConnection con = GetConnection(_cons))
                {
                    // Checks if the string is one word
                    if (Regex.IsMatch(query, @"^\w+$"))
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
        }
        // ############ SELECTS ONLY ONE ROW DATA  ##############################
        public static async Task<string> GetOneData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetConnection(_cons))
                {
                    return await con.QuerySingleOrDefaultAsync<string>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        // ############ GET THE TOTAL COUNT OF THE QUERY ########################
        public static async Task<int> GetCountData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetConnection(_cons))
                {
                    int count = await con.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
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
                using (IDbConnection con = GetConnection(_cons))
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
            catch (Exception ex)
            {
                Debug.WriteLine("SQL Exception: " + ex.Message);
                return false;
            }

        }
        // ############ INSERT AND UPDATE QUERY ########################
        public static async Task<bool> UpdateInsertQuery(string strQuery, object parameters)
        {
            try
            {
                using (IDbConnection con = GetConnection(_cons))
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
                Debug.WriteLine($"Exception: {ex.Message}");

                return false;
            }

        }
        public static async Task<List<string>> StringList(string query, object parameters = null)
        {
            var stringList = new List<string>();

            try
            {
                using (IDbConnection con = GetConnection(_cons))
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
                using (IDbConnection con = GetConnection(_cons))
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
                using (IDbConnection con = GetConnection(_cons))
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