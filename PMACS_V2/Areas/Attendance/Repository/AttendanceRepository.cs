using Dapper;
using PMACS_V2.Areas.Attendance.Interface;
using PMACS_V2.Areas.Attendance.Model;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.Attendance.Repository
{
    public class AttendanceRepository : IAttendance
    {
        public async Task<bool> AttendanceTimeIn(SummaryAttendanceModel sm)
        {
            string insertQuery = "INSERT INTO AttendanceMonitor (Employee_ID, Shifts, LateTime) VALUES (@Employee_ID, @Shifts, @LateTime)";
            var parameters = new { Employee_ID = sm.Employee_ID, Shifts = sm.Shifts, LateTime = sm.LateTime };

            return await SqlDataAccess.UpdateInsertQuery(insertQuery, parameters);
        }
        public async Task<bool> AttendanceTimeOut(SummaryAttendanceModel sm)
        {
            string updateQuery = " UPDATE AttendanceMonitor SET TimeOut = @TimeOut, Regular = @Regular, Overtime = @Overtime, Gtotal = @Gtotal" +
                                         " WHERE CAST(Date_today AS DATE) = @Date_today AND Employee_ID = @Employee_ID";
            var parameters = new { TimeOut = sm.Timeout, Regular = sm.Regular, Overtime = sm.Overtime, Gtotal = sm.Gtotal, Date_today = sm.Date_today, Employee_ID = sm.Employee_ID };

            return await SqlDataAccess.UpdateInsertQuery(updateQuery, parameters);
        }

        public Task<List<AttendanceModel>> CheckAndGetsAttendance()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChecksAttendance()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AttendanceModel>> GetAttendanceRecordsList(string dDate, string shifts, int depid)
        {
            string strquery = "SELECT  pc.Date_today, pc.Employee_ID, e.FullName, " +
                          "FORMAT(pc.TimeIn, 'hh:mm:ss tt') as TimeIn, pc.Shifts, pc.LateTime, " +
                          "FORMAT(pc.TimeOut, 'hh:mm:ss tt') as TimeOut  " +
                          "FROM AttendanceMonitor pc " +
                          "INNER JOIN Employee_tbl e ON e.Employee_ID = pc.Employee_ID " +
                          "WHERE CAST(Date_today AS DATE) = @Datetoday AND Shifts = @Shifts AND e.Department_ID = @Department_ID " +
                          "ORDER BY RecordID DESC";

            var parameters = new { Datetoday = dDate, Shifts = shifts, Department_ID = depid };
            return await SqlDataAccess.GetData<AttendanceModel>(strquery, parameters);
        }
        public async Task<List<SummaryAttendanceModel>> GetAttendanceSummaryList(string tbl, string startDate, string endDate)
        {
            string strquery = "SELECT FORMAT(pc.Date_today, 'MM/dd/yyyy') AS Date_today, " +
                         "pc.Employee_ID, e.FullName, FORMAT(pc.TimeIn, 'HH:mm') as TimeIn, FORMAT(pc.TimeOut, 'HH:mm')  as TimeOut, pc.LateTime, pc.Regular, " +
                         "pc.Overtime, pc.Gtotal, pc.Shifts " +
                         "FROM "+ tbl +" pc " +
                         "INNER JOIN dbo.Employee_tbl e " +
                         "ON e.Employee_ID = pc.Employee_ID " +
                         "WHERE   CAST(Date_today AS DATE) between @startDate AND @endDate";


            var parameters = new { startDate = startDate, endDate = endDate };
            return await SqlDataAccess.GetData<SummaryAttendanceModel>(strquery, parameters);
        }
        public async Task<List<SummaryAttendanceModel>> GetSummaryDataList(string strsql, string startDate, string endDate, string shifts, string search)
        {

            var parameters = new DynamicParameters();
            parameters.Add("startDate", startDate);
            parameters.Add("endDate", endDate);

            if (!string.IsNullOrEmpty(shifts))

                parameters.Add("Shift", shifts);

            if (!string.IsNullOrEmpty(search))
                parameters.Add("Search", search);

            return await SqlDataAccess.GetData<SummaryAttendanceModel>(strsql, parameters);
        }
    }
}