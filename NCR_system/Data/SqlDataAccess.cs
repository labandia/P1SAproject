using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NCR_system.Data;
using Microsoft.Extensions.Caching.Memory;
using System.Configuration;

namespace MSDMonitoring.Data
{
    public sealed class SqlDataAccess
    {
        // -------------------- CONFIG & CACHE --------------------
        private static readonly string _connectionString = BuildConnectionString();
        private static readonly MemoryCache _cache =
         new MemoryCache(new MemoryCacheOptions { SizeLimit = 1024 });


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


                return AesEncryption.DecodeBase64ToString(encrypted);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
                Debug.WriteLine(ex, "GetDataAsync failed.");
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
                Debug.WriteLine(ex, $"GetSingleAsync failed. Query: {query}, CacheKey: {cacheKey}");
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
                Debug.WriteLine(ex, $"SQL Exception while executing query. Query: {query}");
                return "";
            }
        }
        // ############ GET THE TOTAL COUNT OF THE QUERY ########################
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
                Debug.WriteLine(ex, $"ExecuteScalarAsync failed. Query: {query}");
                return 0;
            }
        }

        //public static async Task<double> GetCountByDouble(string query, object parameters = null)
        //{
        //    try
        //    {
        //        using (IDbConnection con = GetConnection(ConnectionString()))
        //        {
        //            double count = 0.0;

        //            if (Regex.IsMatch(query, @"^\w+$"))
        //            {
        //                // This is a Stored Procedure
        //                count = await con.ExecuteScalarAsync<double>(
        //                    query, parameters, commandType: CommandType.StoredProcedure
        //                );
        //            }
        //            else
        //            {
        //                count = await con.ExecuteScalarAsync<double>(
        //                    query, parameters
        //                );
        //            }

        //            return count;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Write(ex.Message);
        //        return 0.0; // must be double now
        //    }
        //}


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
                Debug.WriteLine(ex, $"CheckDataAsync failed. Query: {query}");
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
                Debug.WriteLine(ex, $"ExecuteAsync failed. Query: {query}");
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
                Debug.WriteLine(ex, $"StringListAsync failed. Query: {query}");
                return new List<string>();
            }
        }

       

        // ############ GET DATA BY DATATABLE ##################
        public static async Task<DataTable> GetDataByDataTable(string query, object parameters = null)
        {
            var resultData = new DataTable();
            try
            {
                using (IDbConnection con = CreateConnection())
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
