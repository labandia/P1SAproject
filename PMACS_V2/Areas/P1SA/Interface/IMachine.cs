using PMACS_V2.Areas.P1SA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.P1SA.Interface
{
    public interface IMachine
    {
        //===============  GET FAN MAJOR MACHINE  DATA ===================
        Task<List<MachineModel>> GetMachineData(int offset, int limit, int sect, string mach);
        Task<List<EquipmentList>> GetEquipmentData(int sectionID);
        Task<List<CountMachineModel>> GetCountMachine(int sectionID, string MachineCode);

        //===============  CRUD OPERATION MACHINE DATA ===================
        Task<bool> AddMachine(PostMachineModel model);
        Task<bool> EditMachine(MachineModel model);
        Task<bool> DeleteMachine(int machID);
    }
}
