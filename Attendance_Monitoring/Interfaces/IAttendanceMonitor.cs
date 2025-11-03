using Attendance_Monitoring.Models;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Interfaces
{
    public interface IAttendanceMonitor 
    {
        Task<ApiResponse<P1SA_AttendanceModel>> GetAttendanceRecordsList(string dDate, int shifts, int selectime, int depid);
        Task<ApiResponse<P1SA_AttendanceModel>> GetAttendanceSummaryList(string startDate, string endDate, int depid, string search = "");
        // INSERT INTO THE DATABASE
        Task<ApiResponse<P1SA_AttendanceModel>> AttendanceTimeIn(string EmployeeID, string late);
        // UPDATES INTO THE DATABASE
        Task<ApiResponse<P1SA_AttendanceModel>> AttendanceTimeOut(string EmployeeID);
    }
}
