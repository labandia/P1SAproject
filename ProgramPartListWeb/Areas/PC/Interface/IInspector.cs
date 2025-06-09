using ProgramPartListWeb.Areas.PC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Interface
{
    public interface IInspector
    {
        // GET SCHEDULE TODAY LIST FOR DASHBOARD
        Task<List<PatrolSchedule>> GetScheduleDate();


        // GET DATA FROM THE LIST
        Task<List<Employee>> GetEmployee();
        Task<int> GetEmployeeByDepartment(string employee);
        Task<List<InspectorModel>> GetInpectorsData();
        Task<List<PatrolRegistionModel>> GetRegistrationData();
        Task<List<FindingModel>> GetPatrolFindings(string reg);
        Task<List<CalendarSched>> GetCalendarData(string Employee_ID);
        Task<List<ProccessModel>> GetProcessData(int depid);
        // GET DATA FOR THE LIST TRAINERS AND SCHUDLE
        Task<bool> SetScheduleCalendar(object paramaters, int mode);
        Task<bool> RemoveScheduleCalendar();

        // CRUD OPERATION
        Task<bool> AddEditInpectors(object paramaters, int mode);
        Task<bool> ApproveAndDisapproveInpectors(int inspectID, int status);



        Task<bool> AddRegistration(object paramaters, string json);
        Task<bool> EditRegistration(object paramaters, string json);
        Task<bool> DeleteRegistration(string RegNo);
    }
}
