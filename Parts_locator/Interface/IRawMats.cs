using Parts_locator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Parts_locator.Interface
{
    public interface IRawMats
    {
        Task<List<RawMatModel>> GetRawMatProduct();
        Task<List<RawMatModel>> GetRawMatProductByType(int bush);
        Task<List<RawMatModel>> GetRawMatProductByID(int act);

        // SHOPORDER FUNCTIONS
        Task<List<RawMatSummaryModel>> GetShopOrderlist(int act);

        // MASTERLIST FUNCTIONS
        Task<bool> AddMasterlist(RawMatModel masterlist);
        Task<bool> EditMasterlist(string partnum, int qty, int type);
        Task<bool> DeleteMasterlist(int ID);
        Task<bool> ImportMasterlist(RawMatModel masterlist);
        Task<bool> InsertTransaction(RawMatModel masterlist);

        // UPDATE STORAGE
        Task<bool> UpdateRawMatsQuantity(RawMatInputModel raw);
    }
}
