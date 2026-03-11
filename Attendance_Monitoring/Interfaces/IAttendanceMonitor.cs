using Attendance_Monitoring.Models;
using System;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Interfaces
{
    public interface IAttendanceMonitor 
    {
        Task<ApiResponse<P1SA_AttendanceModel>> GetAttendanceRecordsList(
            string search,
            DateTime StartDate,
            DateTime EndDate,
            int shifts, 
            int selectime, 
            int depid);
        Task<ApiResponse<P1SA_SummaryDataModel>> GetAttendanceSummaryList(string search,
           DateTime StartDate,
           DateTime EndDate,
           int shifts,
           int selectime,
           int depid);
        // INSERT INTO THE DATABASE
        Task<ApiResponse<P1SA_AttendanceModel>> AttendanceTimeIn(string EmployeeID, string late);
        // UPDATES INTO THE DATABASE
        Task<ApiResponse<P1SA_AttendanceModel>> AttendanceTimeOut(string EmployeeID);
    }
}
