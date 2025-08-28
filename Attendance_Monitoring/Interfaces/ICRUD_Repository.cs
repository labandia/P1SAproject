using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Interfaces
{
    public interface ICRUD_Repository<T> where T : class
    {
        Task<List<T>> GetDataList(
            string query,
            object parameters = null,
            string cacheKey = null,
            int cacheMinutes = 10);

        Task<List<TResult>> GetChildrenDataList<TResult>(
             string query,
             object parameters = null) where TResult : class;

        Task<T> GetDataListById(string query, object parameters = null);
        Task<bool> AddUpdateData(object parameters, string query);
        Task<bool> DeleteData(string query, object id);
    }
}
