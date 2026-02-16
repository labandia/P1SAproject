using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    public interface IMetalMast_Transaction
    {
   
        Task<List<MetalMaskTransaction>> GetMetalMaskTransaction(
            string search,
            string partnum,
            int SMTLine,
            int Stats,
            int ModelType,
            int pageNumber,
            int pageSize);

        Task<PagedResult<MetalMaskTransaction>> GetTransactINComplete(
            string partnum, 
            int com,
            int pageNumber,
            int pageSize);

        Task<bool> UpdateMetalMaskIncomplete(MetalMaskTransaction metal);

        Task<MetalMaskTransaction> GetMetalMaskTransacDetails(int RecordID);

        Task<MetalMasKCountTransact> GetTheTotalCount();


        Task<bool> StartOperation(int ID);
        Task<bool> EndOperation(int ID);

        Task<bool> AddMetalMastTransaction(MetalMaskTransaction metal);
        Task<bool> EditMetalMastTransaction(MetalMaskTransaction metal);
        Task<bool> DeleteMetalMastTransaction(int ID);

        Task<bool> SMTsubmitTransaction(MetalMaskTransaction metal);

        Task<bool> TensionsubmitTransaction(MetalMaskTransaction metal);
    }
}
