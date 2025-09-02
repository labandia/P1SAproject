using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Interfaces
{
    public interface ICRmonitorV2
    {
        Task<List<CRmodel>> GetCRMonitoringSummary(int depid, string startDate, string endDate);
        Task<List<CRmodel>> GetCRMonitoringData(string dDate, string shifts, int depid);
        Task<List<CRmodel>> GetCRMonitoringSummaryDatalist(string strsql, string startDate, string endDate, string shifts, string search);

        // INSERT INTO THE DATABASE
        Task<bool> CRTimeGo(string EmployeeID, string shift);
        // UPDATES INTO THE DATABASE
        Task<bool> CRTimeBack(string EmployeeID, DateTime dTimeback, string duration, string datetoday);
    }
}
