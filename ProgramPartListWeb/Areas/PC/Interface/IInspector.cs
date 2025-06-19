using ProgramPartListWeb.Areas.PC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.PC.Interface
{
    public interface IInspector
    {
        // Employee Information
        Task<List<Employee>> GetEmployee();
        Task<int> GetEmployeeByDepartment(string employee);

        // GET SCHEDULE TODAY LIST FOR DASHBOARD
        Task<List<PatrolSchedule>> GetScheduleDate();
        Task<List<CalendarSched>> GetScheduleDateByMonth();


        // INSPECTOR DATA
        Task<List<InspectorModel>> GetInpectorsData();

        // REGISTRATION DATA
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
