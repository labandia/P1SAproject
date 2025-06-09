using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    public interface IAttendance
    {
        Task<List<AttendanceModel>> GetAttendanceRecordsList(string tbl, string dDate, string shifts, int selectime);
        Task<List<SummaryAttendanceModel>> GetAttendanceSummaryList(string tbl, string startDate, string endDate);
        Task<List<SummaryAttendanceModel>> GetSummaryDataList(string strsql, string startDate, string endDate, string shifts, string search);
        // CHECKS THE TIME IN AND OUT IF EXIST
        Task<List<AttendanceModel>> CheckAndGetsAttendance();
        Task<bool> ChecksAttendance();

        // INSERT INTO THE DATABASE
        Task<bool> AttendanceTimeIn(string EmployeeID, string shift, string late, string tb);
        // UPDATES INTO THE DATABASE
        Task<bool> AttendanceTimeOut(SummaryAttendanceModel attend, string tablename);

       
    }
}
