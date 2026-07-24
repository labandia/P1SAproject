using PMACS_V2.Areas.P1SA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.MoldDie.Interface
{
    public interface IDieMasterList
    {
        Task<List<MoldieMasterModel>> GetModelDieMasterList(string search);

        Task<bool> AddMoldieMasterList(MoldieMasterModel model);
        Task<bool> EditMoldieMasterList(MoldieMasterModel model);
        Task<bool> DeleteMoldieMaster(string partno);
    }
}
