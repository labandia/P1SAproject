using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using IssuanceSystem.Services;
using Dapper;
using System.Threading;

namespace IssuanceSystem.Data
{
    public sealed class SqlDataAccess
    {
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


        /// <summary>
        /// Same as the non-generic overload, but allows the work delegate to
        /// return a result (e.g. a newly inserted row's ID) that is passed back
        /// to the caller after the transaction commits successfully.
        /// </summary>
        public static async Task<T> ExecuteInTransactionAsync<T>(
            Func<IDbConnection, IDbTransaction, Task<T>> work)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var result = await work(connection, transaction);

                        transaction.Commit();

                        return result;
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

        public static SqlConnection GetConnection(string connectionString) =>  new SqlConnection(connectionString);
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

        // ############ SELECTS ONLY ONE ROW DATA  ##############################
        public static async Task<string> GetOneData(string query, object parameters)
        {
            try
            {
                using (IDbConnection con = GetConnection(ConnectionString()))
                {
                    return await con.QuerySingleOrDefaultAsync<string>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// Runs a query and manually materializes the results into a
        /// System.Data.DataTable, inferring columns from the first row
        /// returned. Useful for legacy WinForms controls that bind directly to
        /// a DataTable rather than a strongly-typed list.
        /// </summary>
        public static async Task<DataTable> GetDataTableAsync(
            string sql,
            object parameters = null)
        {
            using (var connection = GetConnection())
            {
                var rows = (await connection.QueryAsync(sql, parameters)).ToList();

                var table = new DataTable();

                // No rows returned: give back an empty table with no columns,
                // since there's nothing to infer a schema from.
                if (!rows.Any())
                    return table;

                // Dapper returns dynamic rows backed by IDictionary<string, object>;
                // use the first row to build the column list and types.
                foreach (var col in (IDictionary<string, object>)rows.First())
                {
                    table.Columns.Add(
                        col.Key,
                        col.Value?.GetType() ?? typeof(string));
                }

                foreach (IDictionary<string, object> record in rows)
                {
                    var row = table.NewRow();

                    foreach (var col in record)
                    {
                        row[col.Key] = col.Value ?? DBNull.Value;
                    }

                    table.Rows.Add(row);
                }

                return table;
            }
        }


    }
}
