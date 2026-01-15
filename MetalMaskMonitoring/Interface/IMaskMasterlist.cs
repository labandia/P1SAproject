using MetalMaskMonitoring.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MetalMaskMonitoring.Interface
{
    public interface IMaskMasterlist
    {
        Task<List<MetalMaskModel>> GetMasterlist(string partnum, int Area, int Model, string search);
        Task<bool> AddMasterlist(MetalMaskModel masterlist);
        Task<bool> EditMasterlist(MetalMaskModel masterlist);
    }
}
