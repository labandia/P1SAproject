using PMACS_V2.Areas.P1SA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.MoldDie.Interface
{
    public interface IMoldTooling
    {
        Task<List<DieMoldToolingModel>> GetMoldToolingList(string search, int page = 1, int pageSize = 50);
        Task<bool> AddEditMoldTooling(DieMoldToolingModel model, bool isadd = true);
        Task<bool> DeleteMoldTooling(int recordID);
    }
}
