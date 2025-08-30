using Attendance_Monitoring.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Interfaces
{
    public interface IAttendanceMonitor 
    {
        Task<List<P1SA_AttendanceModel>> GetAttendanceRecordsList(string dDate, int shifts, int selectime, int depid);
        Task<List<P1SA_AttendanceModel>> GetAttendanceSummaryList(string startDate, string endDate, string search = "");
        // INSERT INTO THE DATABASE
        Task<bool> AttendanceTimeIn(string EmployeeID, int shift, string late);
        // UPDATES INTO THE DATABASE
        Task<bool> AttendanceTimeOut(P1SA_AttendanceModel attend);
    }
}
