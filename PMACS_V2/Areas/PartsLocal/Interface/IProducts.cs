using PMACS_V2.Areas.PartsLocal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.PartsLocal.Interface
{
    public interface IProducts
    {
        Task<IEnumerable<RotorProductModel>> GetRotorMasterlist();
        Task<IEnumerable<RotorProductModel>> GetRotorStorage();
        Task<bool> UpdateRotorMasterlist(RotorProductModel rotor);
        Task<bool> AddRotorMasterlist(RotorProductModel rotor);
    }
}
