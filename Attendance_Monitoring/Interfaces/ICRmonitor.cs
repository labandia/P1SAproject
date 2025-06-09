using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    public interface ICRmonitor
    {
        Task<List<CRaccess>> GetCRAccess();
        Task<List<CRmodel>> GetCRMonitoringSummary(int depid, string startDate, string endDate);
        Task<List<CRmodel>> GetCRMonitoringData(string dDate, string shifts, int depid);
        // INSERT INTO THE DATABASE
        Task<bool> CRTimeGo(string EmployeeID, string shift);
        // UPDATES INTO THE DATABASE
        Task<bool> CRTimeBack(string EmployeeID, DateTime dTimeback, string duration, string  datetoday);


        // Exporting 
        Task<List<ExportCRmodel>> GetCRMonitoringSummaryDatalist(string strsql, string startDate, string endDate, string shifts, string search);
    }
}
