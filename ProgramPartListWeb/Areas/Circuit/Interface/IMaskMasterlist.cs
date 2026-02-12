using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    public interface IMaskMasterlist
    {
        Task<PagedResult<MetalMaskModel>> GetMetalMaskMasterlist(
            string search,
            int Area,
            int ModelType,
            int pageNumber,
            int pageSize);

        Task<MetalMaskModel> GetMetalMaskByID(int ID);
        Task<List<MetalMaskModel>> SearchMetalMaskData(string partnum, int model);

        Task<bool> AddMasterlist(MetalMaskModel masterlist);
        Task<bool> EditMasterlist(MetalMaskModel masterlist);
    }
}
