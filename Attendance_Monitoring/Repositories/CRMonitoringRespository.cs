using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    public class CRMonitoringRespository : ICRmonitor
    {
        public Task<bool> CRTimeGo(string EmployeeID, string shift)
        {
            string strquery = "InputCR";
            var parameters = new { Employee_ID = EmployeeID, Shifts = shift };
            return SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }
        public Task<bool> CRTimeBack(string EmployeeID, DateTime dTimeback, string duration, string datetoday)
        {
            string strquery = "UpdateCR";
            var parameters = new { Employee_ID = EmployeeID, Timeout = dTimeback, Duration = duration, DateToday = datetoday };
            return SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }

        public Task<List<CRaccess>> GetCRAccess() => SqlDataAccess.GetData<CRaccess>("CRAccessMonitor");   

        public Task<List<CRmodel>> GetCRMonitoringData(string dDate, string shifts, int depid)
        {
            var parameters = new { TimeIn = dDate, Shifts = shifts, Depid = depid };
            return SqlDataAccess.GetData<CRmodel>("CRMonitor", parameters);
        }

        public Task<List<CRmodel>> GetCRMonitoringSummary(int depid, string startDate, string endDate)
        {
            return SqlDataAccess.GetData<CRmodel>("CRMonitorSummary", new { Depid = depid, startDate = startDate, endDate = endDate });
        }
        public Task<List<ExportCRmodel>> GetCRMonitoringSummaryDatalist(string strsql, string startDate, string endDate, string shifts, string search)
        {

            var parameters = new DynamicParameters();
            parameters.Add("startDate", startDate);
            parameters.Add("endDate", endDate);

            if (!string.IsNullOrEmpty(shifts))
                parameters.Add("Shift", shifts);

            return SqlDataAccess.GetData<ExportCRmodel>(strsql, parameters);
        }
    }
}
