using Attendance_Monitoring.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Interfaces
{
    public interface IAttendanceMonitor 
    {
        Task<List<P1SA_AttendanceModel>> GetAttendanceRecordsList(string dDate, int shifts, int selectime, int depid);
    }
}
