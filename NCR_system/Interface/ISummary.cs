using NCR_system.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    public interface ISummary
    {
        Task<List<CustomerTotalModel>> GetCustomersOpenItem(int type = 0);
    }
}
