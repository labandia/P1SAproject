using ProgramPartListWeb.Areas.Circuit.Models;
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
