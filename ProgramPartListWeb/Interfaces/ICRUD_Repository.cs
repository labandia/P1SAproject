using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
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
        Task<int> AddUpdateData(T entity, string query);
        Task<int> DeleteData(string query, object id);
    }
}
