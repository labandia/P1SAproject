using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    public interface IPlanRepository
    {
        // ==============  PLAN SCHEDULE PAGE ===================== //
        Task<List<PlanPartslist>> GetPlanScheduleData();
        Task<PlanPartslist> GetPlanWithComponentsList(string series);

        // ==============  PLANLIST DATA PAGE ===================== //
        Task<List<PlanPartslist>> GetPlanScheduleManageData(int pagenum, int pagesize, int filter);


    }
}
