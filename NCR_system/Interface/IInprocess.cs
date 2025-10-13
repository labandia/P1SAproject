using NCR_system.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCR_system.Interface
{
    public interface IInprocess
    {
        Task<IEnumerable<InprocessModel>> GetInprocessData(int section);
        Task<bool> InsertInprocessData(InprocessModel inprocess);
        Task<bool> UpdateInprocessData(InprocessModel inprocess);
    }
}
