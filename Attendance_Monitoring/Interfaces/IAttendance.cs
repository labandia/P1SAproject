using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    public interface IAttendance
    {
        Task<List<AttendanceModel>> GetAttendanceRecordsList(string tbl, string dDate, string shifts, int selectime);
        Task<List<SummaryAttendanceModel>> GetAttendanceSummaryList(string tbl, string startDate, string endDate);
        Task<List<SummaryAttendanceModel>> GetSummaryDataList(string strsql, string startDate, string endDate, string shifts, string search);
        // CHECKS THE TIME IN AND OUT IF EXIST
        Task<bool> ChecksAttendance(string EmployeeID, string shift, string tb);

        // INSERT INTO THE DATABASE
        Task<bool> AttendanceTimeIn(string EmployeeID, string shift, string late, string tb);
        // UPDATES INTO THE DATABASE
        Task<bool> AttendanceTimeOut(SummaryAttendanceModel attend, string tablename);

        Task<bool> AttendanceDeleteTimeIN(int ID, DateTime dateToday);
        Task<bool> AttendanceDeleteTimeOut(int ID, DateTime dateToday);
    }
}
