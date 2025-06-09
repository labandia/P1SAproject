using PMACS_V2.Areas.Attendance.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.Attendance.Interface
{
    public interface ICRmonitor
    {
        Task<IEnumerable<CRaccess>> GetCRAccess();
        Task<IEnumerable<CRmodel>> GetCRMonitoringSummary(int depid);
        Task<IEnumerable<CRmodel>> GetCRMonitoringData(string dDate, string shifts, int depid);
        // INSERT INTO THE DATABASE
        Task<bool> CRTimeGo(string EmployeeID, string shift);
        // UPDATES INTO THE DATABASE
        Task<bool> CRTimeBack(string EmployeeID, DateTime dTimeback, string duration, string datetoday);
    }
}
