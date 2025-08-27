using Attendance_Monitoring.Interfaces;
using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    internal class AttendanceMonitorRepository : CRUD_Repository<P1SA_AttendanceModel>, IAttendanceMonitor
    {
        public Task<List<P1SA_AttendanceModel>> GetAttendanceRecordsList(string dDate, int shifts, int selectime, int depid)
        {
            throw new NotImplementedException();
        }
    }
}
