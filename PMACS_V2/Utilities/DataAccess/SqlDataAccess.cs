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
using System.IO;
using ProgramPartListWeb.Utilities;

namespace PMACS_V2.Helper
{
    public static class SqlDataAccess
    {
        // Auto Connection Based on the Domain URL
        public static string _connectionString()
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            string machineName = Environment.MachineName.ToLower();
            string connectionKey = "LiveDevelopment";

            //if (host.Contains("pmacsweb.sdp.com"))
            //{
            //    connectionKey = "LiveDevelopment";
            //}
            
            //if (host.Contains("localhost"))
            //{
            //    if (machineName == "desktop-fc0up1p") // Home PC name
            //        connectionKey = "HomeDevelopment";
            //    else
            //        connectionKey = "TestDevelopment";
            //}


            LogConnectionChoice(host, machineName, connectionKey);

            return AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings[connectionKey].ConnectionString);
        }

        // CHECK CONNECTION 
        private static void LogConnectionChoice(string host, string machineName, string connectionKey)
        {
            try
            {
                //string logFile = HttpContext.Current.Server.MapPath("~/App_Data/connection_log.txt");

                string logEntry = $"{DateTime.Now:u} | Host: {host} | Machine: {machineName} | Connection: {connectionKey}";
                Debug.WriteLine(logEntry);
                //File.AppendAllText(logFile, logEntry + Environment.NewLine);
            }
            catch
            {
                // Silent fail to avoid crashing the app
            }
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
                //CustomLogger.LogError(ex);
                Debug.WriteLine(ex.Message);
                return new List<T>();
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
        public static async Task<int> GetCountData(string query, object parameters = null)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
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
                //CustomLogger.LogError(ex);
                Debug.WriteLine($"Exception in UpdateInsertQuery: {ex.Message}");
                return false;
            }
        }
        // ############ LIST OF STRINGS ########################
        public static async Task<List<string>> GetlistStrings(string query, object parameters = null)
        {
            var stringList = new List<string>();
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    IEnumerable<string> dataList;

                    // Check if the query is a stored procedure (only word characters, no spaces/symbols)
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
















        public static async Task<bool> UploadDataFiles(DataTable dt, int Op)
        {
            var formattedDate = DateTime.Now.Date;
            object dynamicList = null;
            string strquery = "";

            using (IDbConnection conn = GetSqlConnection(_connectionString()))
            {
                if (Op == 0)
                {
                    dynamicList = dt.AsEnumerable().Select(row => new
                    {
                        DOI_Date = ParseDate(row.Field<string>("DOI to M1")),
                        Date_Created = ParseDate(row.Field<string>("Created date")),
                        Branch = row.Field<string>("Branch"),
                        MD_Request = ParseDate(row.Field<string>("MD Based on Sales Request")),
                        SDP_Shoporder = row.Field<string>("SDP_Order No"),
                        SDP_Sales_Partnum = row.Field<string>("SDP_Sales Part No"),
                        SDP_Sales_Number = int.TryParse(row.Field<string>("SDP_Sales Qty"), out var qty) ? qty : (int?)null,
                        Sales_Request_Date = ParseDate(row.Field<string>("Sales Requested Ship date")),
                        Previous_Update = row.Field<string>("Previous Update"),
                        PC_Proposed_Date = row.Field<string>("PC1 Proposed Ship Date"),
                        M1_Latest_Update = row.Field<string>("M1 LATEST UPDATE"),
                        PC_Latest_Proposed_Date = row.Field<string>("PC1 Latest Proposed Ship Date"),
                        Status = row.Field<string>("Reply status"),
                        Partnumber = row.Field<string>("PART NO."),
                        Partname = row.Field<string>("PART NAME"),
                        Usage = double.TryParse(row.Field<string>("Usage"), out var usage) ? usage : (double?)null,
                        Judgement = row.Field<string>("Judgement")
                    }).ToList();

                    strquery = "Importdata";
                }
                else if (Op == 1)
                {
                    dynamicList = dt.AsEnumerable()
                        .Where(row => ParseDate(row.Field<string>("Date Upload")) == formattedDate)
                        .GroupBy(row => row.Field<string>("SDP_Order No"))
                        .Select(group => group.First())
                        .Select(row => new
                        {
                            DOI_Date = ParseDate(row.Field<string>("DOI to M1")),
                            Date_Created = ParseDate(row.Field<string>("Created date")),
                            Branch = row.Field<string>("Branch"),
                            MD_Request = ParseDate(row.Field<string>("MD Based on Sales Request")),
                            SDP_Shoporder = row.Field<string>("SDP_Order No"),
                            SDP_Sales_Partnum = row.Field<string>("SDP_Sales Part No"),
                            SDP_Sales_Number = int.TryParse(row.Field<string>("SDP_Sales Qty"), out var qty) ? qty : (int?)null,
                            Sales_Request_Date = ParseDate(row.Field<string>("Sales Requested Ship date")),
                            Previous_Update = row.Field<string>("Previous Update"),
                            PC_Proposed_Date = row.Field<string>("PC1 Proposed Ship Date"),
                            M1_Latest_Update = row.Field<string>("M1 LATEST UPDATE"),
                            PC_Latest_Proposed_Date = row.Field<string>("PC1 Latest Proposed Ship Date"),
                            Status = row.Field<string>("Reply status"),
                            Partnumber = row.Field<string>("PART NO."),
                            Partname = row.Field<string>("PART NAME"),
                            Usage = double.TryParse(row.Field<string>("Usage"), out var usage) ? usage : (double?)null,
                            Judgement = row.Field<string>("Judgement")
                        }).ToList();

                    strquery = "ImportShopOrderdata";
                }

                try
                {
                    Debug.WriteLine("Operation select : " + strquery);
                    await conn.ExecuteAsync(strquery, dynamicList);
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error while importing data: {ex.Message}", ex);
                    throw new Exception($"Error while importing data: {ex.Message}", ex);
                }
            }
        }

        private static DateTime? ParseDate(string dateString)
        {
            if (!string.IsNullOrWhiteSpace(dateString))
            {
                if (DateTime.TryParse(dateString, out DateTime parsedDate))
                {
                    return parsedDate;
                }
            }
            return null; // Return null if parsing fails
        }

    }
}