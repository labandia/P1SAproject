using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    internal class AttendanceV2Repository : IAttendanceV2
    {
        public Task<bool> AttendanceTimeIn(string EmployeeID, string shift, string late)
        {
            var parameters = new { EmpID = EmployeeID, Shifts = shift, LateTime = late };
            return SqlDataAccess.UpdateInsertQuery("InsertTimeIn", parameters);
        }

        public Task<bool> AttendanceTimeOut(SummaryAttendanceModel sm, string tablename)
        {
            var parameters = new { TimeOut = sm.Timeout, Regular = sm.Regular, Overtime = sm.Overtime, Gtotal = sm.Gtotal, Date_today = sm.Date_today, Employee_ID = sm.Employee_ID };
            return SqlDataAccess.UpdateInsertQuery("InsertTimeOut", parameters);
        }

        public async Task<bool> ChecksAttendance(string EmployeeID, string shift)
        {
            var checkParams = new { EmpID = EmployeeID, Shifts = shift };
            int count = await SqlDataAccess.GetCountData("CheckAttendance", checkParams);

            return (count > 0);
        }

        public Task<List<AttendanceModel>> GetAttendanceRecordsList(string dDate, string shifts, int selectime)
        {
            string strquery = (selectime == 0) ? "AttendanceMonitorIN" : "AttendanceMonitorOUT";
            var parameters = new { Datetoday = dDate, Shifts = shifts };

            return SqlDataAccess.GetData<AttendanceModel>(strquery, parameters);     
        }

        public Task<List<SummaryAttendanceModel>> GetAttendanceSummaryList(string startDate, string endDate)
        {
            return SqlDataAccess.GetData<SummaryAttendanceModel>("SummaryAttendance", new { startDate = startDate, endDate = endDate });
        }

        public Task<List<SummaryAttendanceModel>> GetSummaryDataList(string strsql, string startDate, string endDate, string shifts, string search)
        {
            var parameters = new DynamicParameters();
            parameters.Add("startDate", startDate);
            parameters.Add("endDate", endDate);

            if (!string.IsNullOrEmpty(shifts))

                parameters.Add("Shift", shifts);

            if (!string.IsNullOrEmpty(search))
                parameters.Add("Search", search);

            return SqlDataAccess.GetData<SummaryAttendanceModel>(strsql, parameters);
        }
    }
}
