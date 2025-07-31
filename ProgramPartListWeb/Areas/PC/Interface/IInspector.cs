using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Interface
{
    public interface IInspector
    {
        // Employee Information
        Task<List<Employee>> GetEmployee();
        Task<int> GetEmployeeByDepartment(string employee);
        Task<List<UsersModel>> GetUsersInfoSetting();

        // GET SCHEDULE TODAY LIST FOR DASHBOARD
        Task<List<PatrolSchedule>> GetScheduleDate();
        Task<List<CalendarSched>> GetScheduleDateByMonth();


        // INSPECTOR DATA
        Task<List<InspectorModel>> GetInpectorsData();

        Task<bool> AddEditInpectors(object paramaters, int mode);
        Task<bool> ApproveAndDisapproveInpectors(int inspectID, int status);

        // REGISTRATION DATA
        Task<List<PatrolRegistionModel>> GetRegistrationData();
        Task<List<FindingModel>> GetPatrolFindings(string reg);
        Task<List<CalendarSched>> GetCalendarData(string Employee_ID);
        Task<List<ProccessModel>> GetProcessData(int depid);

        Task<bool> AddRegistration(RegistrationModel reg, string json);
        Task<bool> EditRegistration(RegistrationModel reg, string json);
        Task<bool> DeleteRegistration(string RegNo);

        // GET DATA FOR THE LIST TRAINERS AND SCHUDLE
        Task<bool> SetScheduleCalendar(object paramaters, int mode);
        Task<bool> RemoveScheduleCalendar(int ID);

      
    }
}
