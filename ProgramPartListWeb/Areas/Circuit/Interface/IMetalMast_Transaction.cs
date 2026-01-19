using ProgramPartListWeb.Areas.Circuit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    public interface IMetalMast_Transaction
    {
        Task<List<MetalMaskTransaction>> GetMetalMaskTransaction(
            string search,
            int Stats,
            int ModelType,
            int pageNumber,
            int pageSize);

        Task<MetalMaskTransaction> GetMetalMaskTransacDetails();

        Task<bool> AddMetalMastTransaction(MetalMaskTransaction metal);
        Task<bool> EditMetalMastTransaction(MetalMaskTransaction metal);
        Task<bool> DeleteMetalMastTransaction(int ID);
    }
}
