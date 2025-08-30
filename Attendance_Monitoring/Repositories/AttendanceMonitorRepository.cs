using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    internal class AttendanceMonitorRepository : CRUD_Repository<P1SA_AttendanceModel>, IAttendanceMonitor
    {
        public Task<List<P1SA_AttendanceModel>> GetAttendanceRecordsList(string dDate, int shifts, int selectime, int depid)
        {
            string strquery;

            if(selectime == 0)
            {
                strquery = $@"SELECT
	                        FORMAT(pc.TimeIn, 'MM/dd/yyyy') as Date_today, 
	                        FORMAT(pc.TimeIn, 'hh:mm:ss tt') as TimeIn, 
	                        pc.Shifts,
	                        CASE 
	                        WHEN pc.Shifts = 0 THEN 'DAYSHIFT'
	                        ELSE 'NIGHTSHIFT'
	                        END as Shifts,
	                        pc.Employee_ID, e.FullName, pc.LateTime 
                        FROM P1SA_AttendanceMonitor pc 
                        INNER JOIN Employee_tbl e ON e.Employee_ID = pc.Employee_ID
                        WHERE (CAST(pc.TimeIn AS DATE) = @Datetoday AND pc.Shifts = @Shifts) AND 
                        e.Department_ID = @Department_ID
                        ORDER BY pc.RecordID DESC";
            }
            else
            {
                strquery = $@"SELECT  
                              FORMAT(pc.TimeIn, 'MM/dd/yyyy') as Date_today, 
                              pc.TimeOut, pc.Shifts, pc.LateTime 
                              FROM P1SA_AttendanceMonitor pc 
                              INNER JOIN Employee_tbl e ON e.Employee_ID = pc.Employee_ID 
                              WHERE CAST(pc.TimeIn AS DATE) = @Datetoday 
                              AND (pc.TimeOut is Not null AND pc.Shifts = @Shifts) AND e.Department_ID = @Department_ID
                              ORDER BY pc.TimeOut DESC";
            }

            var parameters = new { Datetoday = dDate, Shifts = shifts, Department_ID = depid };

            return GetDataList(strquery, parameters);

        }
        public Task<List<P1SA_AttendanceModel>> GetAttendanceSummaryList(string startDate, string endDate, string search = "")
        {
            string strquery = $@"SELECT FORMAT(pc.TimeIn, 'MM/dd/yyyy') AS Date_today, 
                                pc.Employee_ID, e.FullName, 
                                FORMAT(pc.TimeIn, 'HH:mm') as TimeIn, 
                                FORMAT(pc.TimeOut, 'HH:mm')  as TimeOut, 
                                pc.LateTime, pc.Regular, 
                                pc.Overtime, pc.Gtotal, pc.Shifts 
                                FROM P1SA_AttendanceMonitor pc 
                                INNER JOIN Employee_tbl e 
                                ON e.Employee_ID = pc.Employee_ID 
                                WHERE   CAST(pc.TimeIn AS DATE) between @startDate AND @endDate";
            var parameters = new { startDate = startDate, endDate = endDate };

            return GetDataList(strquery, parameters);
        }
        public Task<bool> AttendanceTimeIn(string EmployeeID, int shift, string late)
        {
            string insertQuery = $@"INSERT INTO P1SA_AttendanceMonitor 
                    (Employee_ID, Shifts, LateTime) VALUES (@EmpID, @Shifts, @LateTime)";
            var parameters = new { EmpID = EmployeeID, Shifts = shift, LateTime = late };
            return AddUpdateData(parameters, insertQuery);
        }
        public Task<bool> AttendanceTimeOut(P1SA_AttendanceModel attend)
        {
            string updateQuery = $@"UPDATE P1SA_AttendanceMonitor 
                            SET TimeOut = @TimeOut, Regular = @Regular, Overtime = @Overtime, Gtotal = @Gtotal
                            WHERE CAST(TimeIn AS DATE) = @Date_today AND Employee_ID = @Employee_ID";
            var parameters = new { TimeOut = attend.TimeOut, 
                Regular = attend.Regular, Overtime = attend.Overtime, Gtotal = attend.Gtotal, 
                Date_today = attend.TimeIn, Employee_ID = attend.Employee_ID };

            return AddUpdateData(parameters, updateQuery);
        }

    }
}
