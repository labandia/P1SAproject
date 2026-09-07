using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface ISqlDataAccess
    {
        Task<List<T>> GetDataAsync<T>(
        string query,
        object parameters = null,
        CommandType commandType = CommandType.Text,
        string cacheKey = null,
        int cacheMinutes = 10);

        Task<T> GetSingleAsync<T>(
            string query,
            object parameters = null,
            CommandType commandType = CommandType.Text,
            string cacheKey = null,
            int cacheMinutes = 10);

        Task<string> GetOneDataAsync(string query, object parameters);

        Task<int> ExecuteScalarAsync(
            string query,
            object parameters = null,
            CommandType commandType = CommandType.Text);

        Task<bool> CheckDataAsync(
            string query,
            object parameters = null,
            CommandType commandType = CommandType.Text);

        Task<bool> ExecuteAsync(
            string query,
            object parameters = null,
            CommandType commandType = CommandType.Text,
            string cacheKeyToInvalidate = null);

        Task<List<string>> StringListAsync(
            string query,
            object parameters = null,
            CommandType commandType = CommandType.Text);

        Task<bool> SoftDeleteAsync(
            string tableName,
            string keyColumn,
            object keyValue);

        Task BulkInsertAsync<T>(
            string tableName,
            List<T> data);

        void ClearCache(string cacheKey = null);
    }
}
