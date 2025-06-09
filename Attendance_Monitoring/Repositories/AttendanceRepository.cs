using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;
using Dapper;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Attendance_Monitoring.Repositories
{
    public class AttendanceRepository : IAttendance
    {
        public async Task<bool> AttendanceTimeIn(string EmployeeID, string shift, string late, string tb)
        {
            string insertQuery = "INSERT INTO "+ tb +" (Employee_ID, Shifts, LateTime) VALUES (@EmpID, @Shifts, @LateTime)";
            var parameters = new { EmpID = EmployeeID, Shifts = shift, LateTime = late };

            return await SqlDataAccess.UpdateInsertQuery(insertQuery, parameters);
        }
        public async Task<bool> AttendanceTimeOut(SummaryAttendanceModel sm, string tablename)
        {
            string updateQuery = " UPDATE " + tablename + " SET TimeOut = @TimeOut, Regular = @Regular, Overtime = @Overtime, Gtotal = @Gtotal" +
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

        public async Task<List<AttendanceModel>> GetAttendanceRecordsList(string tbl, string dDate, string shifts, int selectime)
        {
            string strquery;

            if (selectime == 0)
            {
                strquery = "SELECT  pc.Date_today, pc.Employee_ID, e.FullName, FORMAT(pc.TimeIn, 'hh:mm:ss tt') as TimeIn, pc.Shifts, pc.LateTime " +
                           "FROM "+ tbl +" pc INNER JOIN Employee_tbl e ON e.Employee_ID = pc.Employee_ID " +
                           "WHERE CAST(Date_today AS DATE) = @Datetoday AND Shifts = @Shifts ORDER BY " +
                           "RecordID DESC";
            }
            else
            {
                strquery = "SELECT   pc.Date_today, "+
                     "pc.Employee_ID, e.FullName, FORMAT(pc.TimeOut, 'hh:mm:ss tt') as TimeIn, " +
                     "pc.Shifts, pc.LateTime FROM " + tbl + " pc INNER JOIN Employee_tbl e ON e.Employee_ID = pc.Employee_ID " +
                     "WHERE CAST(Date_today AS DATE) = '" + dDate +  "' AND pc.TimeOut is Not null AND Shifts = '" + shifts +  "' " +
                     "ORDER BY " +
                     "pc.TimeOut DESC";
            }

            var parameters = new { Datetoday = dDate, Shifts = shifts };
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
