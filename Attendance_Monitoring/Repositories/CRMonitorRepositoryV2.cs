using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    internal class CRMonitorRepositoryV2 : ICRmonitorV2
    {
        public async Task<bool> CRTimeBack(string EmployeeID, DateTime dTimeback, string duration, string datetoday)
        {
            string strquery = "UpdateCR";
            var parameters = new { Employee_ID = EmployeeID, Timeout = dTimeback, Duration = duration, DateToday = datetoday };
            return await SqlDataAccess.ExecuteAsync(strquery, parameters);
        }

        public async Task<bool> CRTimeGo(string EmployeeID, string shift)
        {
            string strquery = "InputCR";
            var parameters = new { Employee_ID = EmployeeID, Shifts = shift };
            return await SqlDataAccess.ExecuteAsync(strquery, parameters);
        }

        public async Task<List<CRmodel>> GetCRMonitoringData(string dDate, string shifts, int depid)
        {
            string strquery = "CRMonitor";
            Debug.WriteLine($@"HERE : {dDate} - Shift : {shifts} - Department : {depid}");
            var parameters = new { TimeIn = dDate, Shifts = shifts, Depid = depid };
            return await SqlDataAccess.GetDataAsync<CRmodel>(strquery, parameters, System.Data.CommandType.StoredProcedure);
        }

        public async Task<List<CRmodel>> GetCRMonitoringSummary(int depid, string startDate, string endDate)
        {
            string strquery = "CRMonitorSummary";
            var parameters = new { Depid = depid, startDate = startDate, endDate = endDate };
            return await SqlDataAccess.GetDataAsync<CRmodel>(strquery, parameters);
        }

        public async Task<List<CRmodel>> GetCRMonitoringSummaryDatalist(string strsql, string startDate, string endDate, string shifts, string search)
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

            return await SqlDataAccess.GetDataAsync<CRmodel>(strsql, parameters);
        }
    }

    //internal class CRMonitorRepositoryV2 : CRUD_Repository<CRmodel>, ICRmonitorV2
    //{
    //    public Task<List<CRmodel>> GetCRMonitoringData(string dDate, string shifts, int depid)
    //    {
    //        var parameters = new { TimeIn = dDate, Shifts = shifts, Depid = depid };
    //        return GetDataList("CRMonitor", parameters);
    //    }

    //    public Task<List<CRmodel>> GetCRMonitoringSummary(int depid, string startDate, string endDate)
    //    {
    //        return GetDataList("CRMonitorSummary", new { Depid = depid, startDate = startDate, endDate = endDate });
    //    }

    //    public Task<List<CRmodel>> GetCRMonitoringSummaryDatalist(string strsql, string startDate, string endDate, string shifts, string search)
    //    {
    //        var parameters = new DynamicParameters();
    //        parameters.Add("startDate", startDate);
    //        parameters.Add("endDate", endDate);

    //        if (!string.IsNullOrEmpty(shifts))
    //            parameters.Add("Shift", shifts);


    //        return GetDataList(strsql, shifts);

    //    }

    //    public Task<bool> CRTimeBack(string EmployeeID, DateTime dTimeback, string duration, string datetoday)
    //    {
    //        var parameters = new { Employee_ID = EmployeeID, Timeout = dTimeback, Duration = duration, DateToday = datetoday };
    //        return AddUpdateData("UpdateCR", parameters);
    //    }

    //    public Task<bool> CRTimeGo(string EmployeeID, string shift)
    //    {
    //        return AddUpdateData("InputCR", new { Employee_ID = EmployeeID, Shifts = shift });
    //    }




    //}
}
