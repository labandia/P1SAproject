using NCR_system.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    public interface IInprocess
    {
        Task<List<InprocessModel>> GetInprocessData(
            string search,
            int departmentID,
            int Stats,
            int pageNumber,
            int pageSize);
        Task<List<CustomerTotalModel>> GetCustomersOpenItem(int type = 0, int sec = 0);
        Task<bool> InsertInprocessData(InprocessModel inprocess);
        Task<bool> UpdateInprocessData(InprocessModel inprocess);
    }
}
