using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;
using Dapper;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    public class CRMonitoringRespository : ICRmonitor
    {
        public async Task<bool> CRTimeBack(string EmployeeID, DateTime dTimeback, string duration, string datetoday)
        {
            string strquery = "UpdateCR";
            var parameters = new { Employee_ID = EmployeeID, Timeout = dTimeback, Duration = duration, DateToday = datetoday};
            return await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }

        public async Task<bool> CRTimeGo(string EmployeeID, string shift)
        {
            string strquery = "InputCR";
            var parameters = new { Employee_ID = EmployeeID, Shifts = shift };
            return await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }

        public async Task<List<CRaccess>> GetCRAccess()
        {
            string strquery = "CRAccessMonitor";
            var parameters = new {};
            return await SqlDataAccess.GetData<CRaccess>(strquery, parameters);
        }

        public async Task<List<CRmodel>> GetCRMonitoringData(string dDate, string shifts, int depid)
        {
            string strquery = "CRMonitor";
            var parameters = new { TimeIn = dDate, Shifts = shifts, Depid = depid };
            return await SqlDataAccess.GetData<CRmodel>(strquery, parameters);
        }

        public async Task<List<CRmodel>> GetCRMonitoringSummary(int depid, string startDate, string endDate)
        {
            string strquery = "CRMonitorSummary";
            var parameters = new {  Depid = depid, startDate = startDate, endDate = endDate };
            return await SqlDataAccess.GetData<CRmodel>(strquery, parameters);
        }

        public async Task<List<ExportCRmodel>> GetCRMonitoringSummaryDatalist(string strsql, string startDate, string endDate, string shifts, string search)
        {
            //MessageBox.Show(formattedStartDate);
            //MessageBox.Show(formattedEndDate);
            //MessageBox.Show(shifts.Text);
            //MessageBox.Show(searchbox.Text);


            var parameters = new DynamicParameters();
            parameters.Add("startDate", startDate);
            parameters.Add("endDate", endDate);

            if (!string.IsNullOrEmpty(shifts))
                parameters.Add("Shift", shifts);

            return await SqlDataAccess.GetData<ExportCRmodel>(strsql, parameters);
        }
    }
}
