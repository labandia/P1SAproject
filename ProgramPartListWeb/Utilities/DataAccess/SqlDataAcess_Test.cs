using System.Collections.Concurrent;
using System.Threading;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NLog;
using CommandType = System.Data.CommandType;
using System.Configuration;
using System.Text.RegularExpressions;

namespace ProgramPartListWeb.Utilities.DataAccess
{
    public  sealed class SqlDataAcess_Test
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly Regex ProcRegex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)?$", RegexOptions.Compiled);

        private static readonly Lazy<string> _connectionString =
         new Lazy<string>(() =>
         {
             var raw = ConfigurationManager.ConnectionStrings["LiveTestDevelopment"]?.ConnectionString;

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


        public static async Task BulkInsertAsync<T>(
            string tableName,
            List<T> data)
        {
            if (data == null || !data.Any())
                return;

            using (var con = GetConnection())
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

        


    

    }
}