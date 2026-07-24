using System.Collections.Concurrent;
using System.Threading;
using System.Data.SqlClient;
//using System.Web.Caching;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Dapper;
using NLog;
using ProgramPartListWeb.Utilities;
using CommandType = System.Data.CommandType;
using System.Configuration;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PMACS_V2.Helper
{
    public static class SqlDataAccess
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly Regex ProcRegex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)?$", RegexOptions.Compiled);

        private static readonly Lazy<string> _connectionString =
         new Lazy<string>(() =>
         {
             var raw = ConfigurationManager.ConnectionStrings["LiveDevelopment"]?.ConnectionString;

             if (string.IsNullOrWhiteSpace(raw))
             {
                 throw new InvalidOperationException(
                     "Connection string 'LiveDevelopment' was not found.");
             }

             return raw;
         });

        public static string ConnectionString() => _connectionString.Value;
        public static SqlConnection GetConnection()
                => new SqlConnection(ConnectionString());


        /// <summary>
        /// Determines whether a SQL command string should be executed as a
        /// stored procedure or as plain text. If the caller explicitly passes
        /// "storedProcedure", that choice wins; otherwise the command text is
        /// checked against StoredProcRegex to infer the type automatically
        /// (e.g. "dbo.MyProc" -> StoredProcedure, "SELECT * FROM ..." -> Text).
        /// </summary>
        private static CommandType GetCommandType(string command, bool? storedProcedure)
        {
            if (storedProcedure.HasValue)
                return storedProcedure.Value
                    ? CommandType.StoredProcedure
                    : CommandType.Text;

            return ProcRegex.IsMatch(command)
                ? CommandType.StoredProcedure
                : CommandType.Text;
        }

        /// <summary>
        /// Opens a connection, begins a transaction, and runs the supplied work
        /// delegate against it. Commits on success; rolls back (best-effort)
        /// and rethrows on any exception. Use this overload when the work
        /// doesn't need to return a value.
        /// </summary>
        public static async Task ExecuteInTransactionAsync(
            Func<IDbConnection, IDbTransaction, Task> work)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await work(connection, transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        try
                        {
                            transaction.Rollback();
                        }
                        catch
                        {
                            // Swallow rollback failures so the original
                            // exception is what propagates to the caller.
                        }

                        throw;
                    }
                }
            }
        }

        public static SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Runs a query and maps every row to type T, returning all results as
        /// a List&lt;T&gt;. Opens and disposes its own connection.
        /// </summary>
        public static async Task<List<T>> QueryAsync<T>(
            string sql,
            object parameters = null,
            bool? isStoredProcedure = null,
            CancellationToken ct = default(CancellationToken))
        {
            using (var connection = GetConnection())
            {
                var command = new CommandDefinition(
                    sql,
                    parameters,
                    commandType: GetCommandType(sql, isStoredProcedure),
                    cancellationToken: ct);

                return (await connection.QueryAsync<T>(command)).AsList();
            }
        }

        /// <summary>
        /// Runs a query expected to return exactly one row and maps it to T.
        /// Throws if zero or more than one row is returned.
        /// </summary>
        public static async Task<T> QuerySingleAsync<T>(
            string sql,
            object parameters = null,
            bool? isStoredProcedure = null,
            CancellationToken ct = default(CancellationToken))
        {
            using (var connection = GetConnection())
            {
                var command = new CommandDefinition(
                    sql,
                    parameters,
                    commandType: GetCommandType(sql, isStoredProcedure),
                    cancellationToken: ct);

                return await connection.QuerySingleAsync<T>(command);
            }
        }

        /// <summary>
        /// Runs a query expected to return zero or one row and maps it to T.
        /// Returns default(T) if no row is found. Throws if more than one row
        /// is returned.
        /// </summary>
        public static async Task<T> QuerySingleOrDefaultAsync<T>(
            string sql,
            object parameters = null,
            bool? isStoredProcedure = null,
            CancellationToken ct = default(CancellationToken))
        {
            using (var connection = GetConnection())
            {
                var command = new CommandDefinition(
                    sql,
                    parameters,
                    commandType: GetCommandType(sql, isStoredProcedure),
                    cancellationToken: ct);

                return await connection.QuerySingleOrDefaultAsync<T>(command);
            }
        }


        /// <summary>
        /// Executes a non-query command (INSERT/UPDATE/DELETE/DDL/stored proc)
        /// using its own connection and returns the number of affected rows.
        /// </summary>
        public static async Task<int> ExecuteAsync(
            string sql,
            object parameters = null,
            bool? isStoredProcedure = null,
            CancellationToken ct = default(CancellationToken))
        {
            using (var connection = GetConnection())
            {
                var command = new CommandDefinition(
                    sql,
                    parameters,
                    commandType: GetCommandType(sql, isStoredProcedure),
                    cancellationToken: ct);

                return await connection.ExecuteAsync(command);
            }
        }

        /// <summary>
        /// Executes a non-query command against an existing connection and
        /// transaction (for use inside ExecuteInTransactionAsync work
        /// delegates), returning the number of affected rows.
        /// </summary>
        public static async Task<int> ExecuteAsync(
            IDbConnection connection,
            IDbTransaction transaction,
            string sql,
            object parameters = null)
        {
            return await connection.ExecuteAsync(
                sql,
                parameters,
                transaction);
        }

        /// <summary>
        /// Executes a query and returns a single scalar value of type T (e.g.
        /// a COUNT(*) result or a newly generated identity), using its own
        /// connection.
        /// </summary>
        public static async Task<T> ExecuteScalarAsync<T>(
            string sql,
            object parameters = null,
            bool? isStoredProcedure = null,
            CancellationToken ct = default(CancellationToken))
        {
            using (var connection = GetConnection())
            {
                var command = new CommandDefinition(
                    sql,
                    parameters,
                    commandType: GetCommandType(sql, isStoredProcedure),
                    cancellationToken: ct);

                return await connection.ExecuteScalarAsync<T>(command);
            }
        }

        /// <summary>
        /// Same as the other ExecuteScalarAsync overload, but runs against an
        /// existing connection/transaction instead of opening a new one.
        /// </summary>
        public static async Task<T> ExecuteScalarAsync<T>(
            IDbConnection connection,
            IDbTransaction transaction,
            string sql,
            object parameters = null)
        {
            return await connection.ExecuteScalarAsync<T>(
                sql,
                parameters,
                transaction);
        }

        public static async Task<bool> ExistsAsync(
           string sql,
           object parameters = null,
           bool? isStoredProcedure = null)
        {
            return await ExecuteScalarAsync<int>(
                sql,
                parameters,
                isStoredProcedure) > 0;
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
                using (var con = GetConnection())
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
                Logger.Error(ex, $"StringListAsync failed. Query: {query}");
                return new List<string>();
            }
        }


        // ############ GET DATA BY DATATABLE ##################
        public static async Task<DataTable> GetDataByDataTable(string query, object parameters = null)
        {
            var resultData = new DataTable();
            try
            {
                using (IDbConnection con = GetConnection())
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
                Debug.WriteLine(ex.Message);
                Logger.Error(ex, $"SQL Exception while executing query. Query: {query}");
            }

            return resultData;
        }



        // -------------------- SOFT DELETE --------------------------
        //public static async Task<bool> SoftDeleteAsync(
        //    string tableName,
        //    string keyColumn,
        //    object keyValue)
        //{
        //    string sql = $"UPDATE {tableName} SET IsDelete = 1 WHERE {keyColumn} = @Id";

        //    return await ExecuteAsync(sql, new { Id = keyValue });
        //}

        //public static async Task BulkInsertAsync<T>(
        //    string tableName,
        //    List<T> data)
        //{
        //    if (data == null || !data.Any())
        //        return;

        //    using (var con = new SqlConnection(_connectionString))
        //    {
        //        await con.OpenAsync();

        //        using (var bulkCopy = new SqlBulkCopy(con))
        //        {
        //            bulkCopy.DestinationTableName = tableName;

        //            var table = new DataTable();
        //            var properties = typeof(T).GetProperties();

        //            foreach (var prop in properties)
        //            {
        //                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        //            }

        //            foreach (var item in data)
        //            {
        //                var row = table.NewRow();
        //                foreach (var prop in properties)
        //                {
        //                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
        //                }
        //                table.Rows.Add(row);
        //            }

        //            await bulkCopy.WriteToServerAsync(table);
        //        }
        //    }
        //}

        // -------------------- CACHE UTILITIES --------------------
        //public static void ClearCache(string cacheKey = null)
        //{
        //    if (!string.IsNullOrEmpty(cacheKey))
        //    {
        //        _cache.Remove(cacheKey);
        //    }
        //    else
        //    {
        //        foreach (var item in _cache)
        //            _cache.Remove(item.Key);
        //    }
        //}


        public static void StartSqlDependency()
        {
            SqlDependency.Start(_connectionString.Value);
        }

        public static void StopSqlDependency()
        {
            SqlDependency.Stop(_connectionString.Value);
        }











        public static async Task<bool> UploadDataFiles(DataTable dt, int Op)
        {
            var formattedDate = DateTime.Now.Date;
            object dynamicList = null;
            string strquery = "";

            using (IDbConnection conn = GetConnection())
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

        public static Task<T> GetByteAsync<T>(string sql, object value)
        {
            throw new NotImplementedException();
        }
    }
}