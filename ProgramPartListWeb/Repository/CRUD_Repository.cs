using ProgramPartListWeb.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProgramPartListWeb.Utilities;
using ProgramPartListWeb.Helper;
using NLog;
using System.Linq;

namespace ProgramPartListWeb.Repository
{
    public class CRUD_Repository<T> : ICRUD_Repository<T> where T : class
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public async Task<List<T>> GetDataList(
            string query,
            object parameters = null,
            string cacheKey = null,
            int cacheMinutes = 10)
        {
            try
            {
                bool IsStoredProcedure = Regex.IsMatch(query, @"^\w+$", RegexOptions.IgnoreCase);

                async Task<List<T>> FetchData()
                {
                    using (IDbConnection con = SqlDataAccess.CreateConnection())
                    {
                        var commandType = IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
                        var result = await con.QueryAsync<T>(query, parameters, commandType: commandType);
                        return result.ToList();
                    }
                }

                if (!string.IsNullOrEmpty(cacheKey))
                {
                    return await CacheHelper.GetOrSetAsync(cacheKey, FetchData, cacheMinutes);
                }


                return await FetchData();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex, $"SQL Exception in {nameof(GetDataList)} | Query: {query} | CacheKey: {cacheKey}");
                return new List<T>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unexpected error in {nameof(GetDataList)} | Query: {query}");
                return new List<T>();
            }
        }


        public async Task<List<TResult>> GetChildrenDataList<TResult>(
            string query, 
            object parameters = null) where TResult : class
        {
            try
            {
                bool IsStoredProcedure = Regex.IsMatch(query, @"^\w+$", RegexOptions.IgnoreCase);

                async Task<List<TResult>> FetchData()
                {
                    using (IDbConnection con = SqlDataAccess.CreateConnection())
                    {
                        var commandType = IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

                        var result = await con.QueryAsync<TResult>(query, parameters, commandType: commandType);
                        return result.ToList(); // convert IEnumerable<TResult> to List<TResult>
                    }
                }

                return await FetchData();
            }
            catch (SqlException ex)
            {
                _logger.Error(ex, $"SQL Exception in {nameof(GetChildrenDataList)} | Query: {query}");
                return new List<TResult>();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unexpected error in {nameof(GetChildrenDataList)} | Query: {query}");
                return new List<TResult>();
            }
        }



        public async Task<T> GetDataListById(string query,  object parameters = null)
        {
            try
            {
                bool IsStoredProcedure = Regex.IsMatch(query, @"^\w+$", RegexOptions.IgnoreCase);

                using (IDbConnection con = SqlDataAccess.CreateConnection())
                {
                    var commandType = IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

                    // Query single or default
                    var result = await con.QuerySingleOrDefaultAsync<T>(query, parameters, commandType: commandType);
                    return result;
                }
            }
            catch (SqlException ex)
            {
                _logger.Error(ex, $"SQL Exception in {nameof(GetDataListById)} | Query: {query}");
                return null; // Return null if not found or error
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unexpected error in {nameof(GetDataListById)} | Query: {query}");
                return null; // Return null if not found or error
            }
        }
        public async Task<int> AddUpdateData(T entity, string query)
        {
            try
            {
                using (IDbConnection con = SqlDataAccess.CreateConnection())
                {
                    // Determine if it's a stored procedure
                    bool IsStoredProcedure = Regex.IsMatch(query, @"^\w+$", RegexOptions.IgnoreCase);
                    var commandType = IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

                    // Execute query and return affected rows
                    int rowsAffected = await con.ExecuteAsync(query, entity, commandType: commandType);
                    return rowsAffected;
                }
            }
            catch (SqlException ex)
            {
                _logger.Error(ex, $"SQL Exception in {nameof(AddUpdateData)} | Query: {query}");
                return 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unexpected error in {nameof(AddUpdateData)} | Query: {query}");
                return 0;
            }
        }
        public async Task<int> DeleteData(string query, object id)
        {
            try
            {
                using (IDbConnection con = SqlDataAccess.CreateConnection())
                {
                    // Determine if it's a stored procedure
                    bool IsStoredProcedure = Regex.IsMatch(query, @"^\w+$", RegexOptions.IgnoreCase);
                    var commandType = IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;

                    // Execute query and return affected rows
                    int rowsAffected = await con.ExecuteAsync(query, id, commandType: commandType);
                    return rowsAffected;
                }
            }
            catch (SqlException ex)
            {
                _logger.Error(ex, $"SQL Exception in {nameof(DeleteData)} | Query: {query}");
                return 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unexpected error in {nameof(DeleteData)} | Query: {query}");
                return 0;
            }
        }

        
    }
}