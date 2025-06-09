using PMACS_V2.Areas.Attendance.Interface;
using PMACS_V2.Areas.Attendance.Model;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PMACS_V2.Areas.Attendance.Repository
{
    public class EmployeeRepository : IEmployee
    {
        public async Task<bool> AddEmployee(Employee emp)
        {
            string strquery = "AddEmployee";
            var parameters = new
            {
                Employee_ID = emp.EmployeeID,
                FullName = emp.Fullname,
                Process = emp.Process,
                Affiliation = emp.Affiliation,
                Department_ID = emp.Department_ID
            };
            return await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }

        public async Task<bool> DeleteEmployee(string employee)
        {
            string strquery = "DeleteEmployee";
            var parameters = new { EmployeeID = employee };
            return await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }

        public async Task<List<Department>> GetDepartments()
        {
            string strquery = "SELECT Department_ID, Department_name FROM Department_tbl";
            return await SqlDataAccess.GetData<Department>(strquery);
        }

        public async Task<List<Employee>> GetEmployees()
        {
            string strquery = "ManageEmployee";
            return await SqlDataAccess.GetData<Employee>(strquery);
        }

        public async Task<bool> UpdateEmployee(Employee emp, string emptemp)
        {
            string strquery = "UpdateEmployee";
            var parameters = new
            {
                CurrentID = emptemp,
                NewID = emp.EmployeeID,
                FullName = emp.Fullname,
                Process = emp.Process,
                Affiliation = emp.Affiliation,
                Department_ID = emp.Department_ID
            };
            return await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }

        public Task<bool> UploadEmployee(DataTable td, int depid, int method)
        {
            throw new NotImplementedException();
        }
    }
}