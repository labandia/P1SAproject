using PMACS_V2.Areas.Attendance.Interface;
using PMACS_V2.Areas.Attendance.Model;
using PMACS_V2.Areas.P1SA.Models;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PMACS_V2.Areas.Attendance.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendance _attend;
        private readonly IEmployee _emp;
        public AttendanceController(IAttendance attend, IEmployee emp)
        {
            _attend = attend;   
            _emp = emp; 
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployeesList(int depid)
        {
            try
            {
                var data = await _emp.GetEmployees() ?? new List<Employee>();
                var filterdata = data.FindAll(e => e.Department_ID == depid);
                var formdata = GlobalUtilities.GetDataMessage(filterdata);
                return Json(formdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(GlobalUtilities.GetErrorMessage(ex), JsonRequestBehavior.AllowGet);
            }    
        }

        [HttpGet]
        public async Task<ActionResult> AttendanceTimeInandOutList(int departmentID, int selectime)
        {
            string timecheck;
            string Timeout_date;
            string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            DateTime currentTime = DateTime.Today.AddDays(-1);
            string yest = currentTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string shift = GlobalUtilities.GetTheShiftSchedule();
            Timeout_date = shift == "DAYSHIFT" ? tdate : yest;
            timecheck = selectime == 0 ? tdate : Timeout_date;

            var data = await _attend.GetAttendanceRecordsList(timecheck, shift, departmentID);
            var formdata = GlobalUtilities.GetDataMessage(data);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public async Task<ActionResult> AttendanceTimeINandOut(string Employee_ID, int SelectTime)
        {
            bool result;
            string shift = GlobalUtilities.GetTheShiftSchedule();

            // ATTENDANCE TIME IN
            if(SelectTime == 1)
            {
                var obj = new SummaryAttendanceModel
                {
                    Employee_ID  = Employee_ID,
                    Shifts = shift,
                    LateTime = GlobalUtilities.CalculateLateTime()
                };

                result = await _attend.AttendanceTimeIn(obj);
            }
            else
            {
                // ATTENDANCE TIME OUT
                var obj = new SummaryAttendanceModel
                {
                    Date_today = DateTime.Now.ToString(),
                    Employee_ID  = Employee_ID,
                    Timeout = "",
                    Regular = 7.67,
                    Overtime = 1,
                    Gtotal = 1
                };

                result = await _attend.AttendanceTimeOut(obj);
            }

            int mode = SelectTime == 1 ? 1 : 2;
            var formdata = GlobalUtilities.GetMessageResponse(result, mode);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> EmployeeManage(string Employee_ID, int SelectTime)
        {
            await Task.Delay(100);
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}