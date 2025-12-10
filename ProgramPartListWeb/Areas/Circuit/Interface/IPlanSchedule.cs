using ProgramPartListWeb.Areas.Circuit.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Interface
{
    public interface IPlanSchedule
    {
        Task<List<PlanScheduleMode>> GetPlanSchedules();
        Task<PlanScheduleMode> GetPlanSchedulesByID(string plan);

        Task<bool> AddPlanSchedules(PlanScheduleMode plan);
        Task<bool> EditPlanSchedules(PlanScheduleMode plan);
        Task<bool> DeletePlanSched(string plan);
    }
}
