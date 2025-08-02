using Attendance_Monitoring.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Interfaces
{
    public interface IAttendanceV2
    {
        Task<List<AttendanceModel>> GetAttendanceRecordsList(string dDate, int shifts, int selectime, int depid);
        Task<List<SummaryAttendanceModel>> GetAttendanceSummaryList(string startDate, string endDate);
        Task<List<SummaryAttendanceModel>> GetSummaryDataList(string strsql, string startDate, string endDate, string shifts, string search);
        // CHECKS THE TIME IN AND OUT IF EXIST
        Task<bool> ChecksAttendance(string EmployeeID, string shift);

        // INSERT INTO THE DATABASE
        Task<bool> AttendanceTimeIn(string EmployeeID, string shift, string late);
        // UPDATES INTO THE DATABASE
        Task<bool> AttendanceTimeOut(SummaryAttendanceModel attend, string tablename);
    }
}
