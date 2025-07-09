using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Attendance_Monitoring.Repositories
{
    public class AttendanceRepository : IAttendance
    {
        public Task<bool> AttendanceTimeIn(string EmployeeID, string shift, string late, string tb)
        {
            string insertQuery = $@"INSERT INTO {tb} (Employee_ID, Shifts, LateTime) VALUES (@EmpID, @Shifts, @LateTime)";
            var parameters = new { EmpID = EmployeeID, Shifts = shift, LateTime = late };

            return SqlDataAccess.UpdateInsertQuery(insertQuery, parameters);
        }
        public Task<bool> AttendanceTimeOut(SummaryAttendanceModel sm, string tablename)
        {
            string updateQuery = " UPDATE " + tablename + " SET TimeOut = @TimeOut, Regular = @Regular, Overtime = @Overtime, Gtotal = @Gtotal" +
                                         " WHERE CAST(Date_today AS DATE) = @Date_today AND Employee_ID = @Employee_ID";
            var parameters = new { TimeOut = sm.Timeout, Regular = sm.Regular, Overtime = sm.Overtime, Gtotal = sm.Gtotal, Date_today = sm.Date_today, Employee_ID = sm.Employee_ID };

            return SqlDataAccess.UpdateInsertQuery(updateQuery, parameters);
        }

  
        public async Task<bool> ChecksAttendance(string EmployeeID, string shift, string tb)
        {
            string checkQuery = $@"SELECT COUNT(*) 
                                  FROM {tb} WHERE Employee_ID = @EmpID 
                                  AND CAST(Date_today AS DATE) = CAST(GETDATE() AS DATE) 
                                  AND Shifts = @Shifts";

            var checkParams = new { EmpID = EmployeeID, Shifts = shift };
            int count = await SqlDataAccess.GetCountData(checkQuery, checkParams);

            return (count > 0);
        }

        public Task<List<AttendanceModel>> GetAttendanceRecordsList(string tbl, string dDate, string shifts, int selectime)
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
            return SqlDataAccess.GetData<AttendanceModel>(strquery, parameters);
        }
        public Task<List<SummaryAttendanceModel>> GetAttendanceSummaryList(string tbl, string startDate, string endDate)
        {
            string strquery = $@"SELECT FORMAT(pc.Date_today, 'MM/dd/yyyy') AS Date_today, 
                         pc.Employee_ID, e.FullName, FORMAT(pc.TimeIn, 'HH:mm') as TimeIn, FORMAT(pc.TimeOut, 'HH:mm')  as TimeOut, pc.LateTime, pc.Regular, 
                         pc.Overtime, pc.Gtotal, pc.Shifts 
                         FROM {tbl} pc 
                         INNER JOIN Employee_tbl e 
                         ON e.Employee_ID = pc.Employee_ID 
                         WHERE   CAST(Date_today AS DATE) between @startDate AND @endDate";
            return SqlDataAccess.GetData<SummaryAttendanceModel>(strquery, new { startDate = startDate, endDate = endDate });
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
