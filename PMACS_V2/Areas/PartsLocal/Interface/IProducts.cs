using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.PartsLocal.Interface
{
    public interface IProducts
    {
        Task<PagedResult<RotorProductModel>> GetRotorMasterlistPage(
            string search,
            int pageNumber,
            int pageSize
        );

        Task<IEnumerable<RotorProductModel>> GetRotorMasterlist();
        Task<List<RotorProductModel>> GetRotorStorage();
        Task<RotorProductModel> GetRotorStorageByID(int ID);
        Task<bool> UpdateRotorMasterlist(RotorProductModel rotor);
        Task<bool> AddRotorMasterlist(RotorProductModel rotor);
    }
}
