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
using NLog;
using ProgramPartListWeb.Utilities;

namespace PMACS_V2.Helper
{
    public static class SqlDataAccess
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        // Auto Connection Based on the Domain URL
        public static string _connectionString()
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            string Hostname = Environment.MachineName.ToLower();
            string connectionKey = "";

            if (host.Contains("pmacsweb.sdp.com"))
            {
                connectionKey = "LiveDevelopment";
            }

            if (host.Contains("localhost"))
            {
                connectionKey = Hostname == "desktop-fc0up1p"
                                          ? "HomeDevelopment"
                                          : "TestDevelopment";
            }


            //LogConnectionChoice(host, Hostname, connectionKey);

            return AesEncryption.DecodeBase64ToString(ConfigurationManager.ConnectionStrings["LiveDevelopment"].ConnectionString);
        }

        // CHECK CONNECTION 
        private static void LogConnectionChoice(string host, string machineName, string connectionKey)
        {
            string logEntry = $"{DateTime.Now:u} | Host: {host} | Machine: {machineName} | Connection: {connectionKey}";
            Debug.WriteLine(logEntry);
        }
        public static SqlConnection GetSqlConnection(string connectionString) => new SqlConnection(connectionString);
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
                            bool IsStoreProd = Regex.IsMatch(query, @"^\w+$");
                            var commandType = IsStoreProd ? CommandType.StoredProcedure : CommandType.Text;
                            var result = await con.QueryAsync<T>(query, parameters, commandType: commandType);

                            return result.ToList();
                        }
                    }, cacheMinutes);
                }
                else
                {
                    // No caching
                    using (IDbConnection con = GetSqlConnection(_connectionString()))
                    {
                        bool IsStoreProd = Regex.IsMatch(query, @"^\w+$");
                        var commandType = IsStoreProd ? CommandType.StoredProcedure : CommandType.Text;
                        var result = await con.QueryAsync<T>(query, parameters, commandType: commandType);

                        return result.ToList();
                    }
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}, CacheKey: {cacheKey}");
                return new List<T>();
            }
        }
        public static async Task<byte[]> GetByteAsync(string query, object parameters = null)
        {
            try
            {
                using (IDbConnection con = GetSqlConnection(_connectionString()))
                {
                    // Always treat as plain SQL text
                    var result = await con.QueryAsync<byte[]>(query, parameters, commandType: CommandType.Text);
                    return result.FirstOrDefault(); // Return first row or null
                }
            }
            catch (SqlException ex)
            {
                Logger.Error(ex, $"SQL Exception while executing query: {query}");
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
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
                return "";
            }
        }

        // ############ SELECTS ONLY ONE ROW DATA  ##############################
        public static async Task<T> GetDataByID<T>(string query, object parameters)
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
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
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
            catch(Exception ex)
            {
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
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
                   
                    bool isStoredProcedure = Regex.IsMatch(strQuery, @"^\w+$");
                    CommandType commandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

                    int rowsAffected = await con.ExecuteAsync(strQuery, parameters, commandType: commandType);

                    // If update was successful and a cache key is provided, remove it
                    if (rowsAffected > 0 && !string.IsNullOrEmpty(cacheKeyToInvalidate)) CacheHelper.Remove(cacheKeyToInvalidate);
                   
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error Message: " + ex.Message);
                Logger.Error(ex, $"SQL Exception : {ex.Message}");
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
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
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
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
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

        internal static Task<T> GetByteAsync<T>(string sql, object value)
        {
            throw new NotImplementedException();
        }
    }
}