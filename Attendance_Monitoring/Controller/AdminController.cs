using Attendance_Monitoring.Models;
using Attendance_Monitoring.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Controller
{
    public class AdminController
    {
        private readonly EmployeeRespository _emp;
        private readonly AttendanceRepository _attend;
        private readonly CRMonitoringRespository _cr;

        public AdminController()
        {
            _emp = new EmployeeRespository();
            _attend = new AttendanceRepository();
            _cr = new CRMonitoringRespository();
        }

        // -------------  Employees Management Process --------------
        public async Task<IEnumerable<Employee>> GetAllEmployees() => await _emp.GetEmployees();
        public async Task<IEnumerable<Department>> GetDepartmentsList() => await _emp.GetDepartments();
        public async Task<bool>AddEmployee(Employee emp) => await _emp.AddEmployee(emp);
        public async Task<bool>UpdateEmployee(Employee emp, string temp) => await _emp.UpdateEmployee(emp, temp);
        public async Task<bool>DeleteEmployee(string emp) => await _emp.DeleteEmployee(emp);


        // -------------  Attendance Management Process --------------
        public async Task<List<AttendanceModel>> GetAttendanceMonitor(string tblname, string datetoday, string shift, int selectime) => await _attend.GetAttendanceRecordsList(tblname, datetoday, shift, selectime);
        public async Task<IEnumerable<SummaryAttendanceModel>> GetSummaryList(string tblname, string startdate, string endDate) => await _attend.GetAttendanceSummaryList(tblname, startdate, endDate);
        public async Task<IEnumerable<SummaryAttendanceModel>> GetExportSummaryList(string strsql, string startdate, string endDate, string shifts, string search) => await _attend.GetSummaryDataList(strsql, startdate, endDate, shifts, search);
        public async Task<bool> AttendanceTimeINandOut(string EmployeeID, string shift, string late, string tb) => await _attend.AttendanceTimeIn(EmployeeID, shift, late, tb);
        public async Task<bool> AttendanceOut(SummaryAttendanceModel attend, string tablename) => await _attend.AttendanceTimeOut(attend, tablename);

        // -------------  CR Monitoring Process --------------
        public async Task<List<CRaccess>> GetCRaccess() => await _cr.GetCRAccess();
        public async Task<List<CRmodel>> GetCRMonitorlist(string dDate, string shifts, int depid) => await _cr.GetCRMonitoringData(dDate, shifts, depid);
        public async Task<List<CRmodel>> GetCRMonitorSummarylist(int depid, string startDate, string endDate) => await _cr.GetCRMonitoringSummary(depid, startDate, endDate);
        public async Task<bool> CRMonitoringIN(string EmployeeID, string shift) => await _cr.CRTimeGo(EmployeeID, shift);
        public async Task<bool> CRMonitoringOut(string EmployeeID, DateTime dTimeback, string duration, string datetoday) => await _cr.CRTimeBack(EmployeeID, dTimeback, duration, datetoday);
        public async Task<List<ExportCRmodel>> GetExportCRMonitorSummarylist(string strsql, string startdate, string endDate, string shifts, string search) => await _cr.GetCRMonitoringSummaryDatalist(strsql, startdate, endDate, shifts, search);
    }
}
