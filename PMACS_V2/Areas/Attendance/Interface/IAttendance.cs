using PMACS_V2.Areas.Attendance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.Attendance.Interface
{
    public interface IAttendance
    {
        Task<List<AttendanceModel>> GetAttendanceRecordsList(string dDate, string shifts, int depid);
        // INSERT INTO THE DATABASE
        Task<bool> AttendanceTimeIn(SummaryAttendanceModel attend);
        // UPDATES INTO THE DATABASE
        Task<bool> AttendanceTimeOut(SummaryAttendanceModel attend);


        Task<List<SummaryAttendanceModel>> GetAttendanceSummaryList(string tbl, string startDate, string endDate);
        Task<List<SummaryAttendanceModel>> GetSummaryDataList(string strsql, string startDate, string endDate, string shifts, string search);
        // CHECKS THE TIME IN AND OUT IF EXIST
        Task<List<AttendanceModel>> CheckAndGetsAttendance();
        Task<bool> ChecksAttendance();

       
    }
}
