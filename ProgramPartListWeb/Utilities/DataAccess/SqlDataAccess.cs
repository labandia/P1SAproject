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
using System.Text.RegularExpressions;

namespace ProgramPartListWeb.Helper
{
    public sealed class SqlDataAccess
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static readonly Regex ProcRegex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)?$", RegexOptions.Compiled);

        // -------------------- CONFIG & CACHE --------------------
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

        // -------------------- SOFT DELETE --------------------------
        public static async Task<bool> SoftDeleteAsync(
            string tableName,
            string keyColumn,
            object keyValue)
        {
            string sql = $"UPDATE {tableName} SET IsDelete = 1 WHERE {keyColumn} = @Id";

            int rows = await ExecuteAsync(sql, new { Id = keyValue });
            return rows > 0;
        }

        public static async Task BulkInsertAsync<T>(
            string tableName,
            List<T> data)
        {
            if (data == null || !data.Any())
                return;

            using (var con = new SqlConnection(_connectionString.Value))
            {
                await con.OpenAsync();

                using (var bulkCopy = new SqlBulkCopy(con))
                {
                    bulkCopy.DestinationTableName = tableName;

                    var table = new DataTable();
                    var properties = typeof(T).GetProperties();

                    foreach (var prop in properties)
                    {
                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    }

                    foreach (var item in data)
                    {
                        var row = table.NewRow();
                        foreach (var prop in properties)
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        table.Rows.Add(row);
                    }

                    await bulkCopy.WriteToServerAsync(table);
                }
            }
        }

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
    }
}