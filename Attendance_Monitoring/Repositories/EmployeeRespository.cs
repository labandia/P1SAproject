
using Attendance_Monitoring.Utilities;
using Attendance_Monitoring.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data;

namespace Attendance_Monitoring.Models
{
    public class EmployeeRespository : IEmployee
    {
        public Task<List<Employee>> GetEmployees()
        {
            string strquery = "ManageEmployee";
            return SqlDataAccess.GetData<Employee>(strquery);
        }
        public Task<List<Department>> GetDepartments()
        {
            string strquery = "SELECT Department_ID, Department_name FROM Department_tbl";
            return  SqlDataAccess.GetData<Department>(strquery);
        }
        public Task<bool> AddEmployee(Employee emp)
        {
            string strquery = "AddEmployee";
            var parameters = new
            {
                Employee_ID = emp.Employee_ID,
                FullName = emp.Fullname,
                Process = emp.Process,
                Affiliation = emp.Affiliation,
                Department_ID = emp.Department_ID
            };
            return SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }
        public Task<bool> DeleteEmployee(string employee)
        {
            string strquery = "DeleteEmployee";
            var parameters = new { EmployeeID = employee };
            return SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }
        public Task<bool> UpdateEmployee(Employee emp, string emptemp)
        {
            string strquery = "UpdateEmployee";
            var parameters = new
            {
                CurrentID = emptemp,
                NewID = emp.Employee_ID,
                FullName = emp.Fullname,
                Process = emp.Process,
                Affiliation = emp.Affiliation,
                Department_ID = emp.Department_ID
            };
            return SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }

        public async Task<bool> UploadEmployee(DataTable td, int depid, int method)
        {
            bool result = true;
            // Clone structure for new entries
            DataTable newRows = td.Clone();

            if(method  == 0)
            {
                await SqlDataAccess.UpdateInsertQuery("DELETE FROM Employee_tbl WHERE Department_ID =@Department_ID", new { Department_ID = depid });
            }

            foreach (DataRow row in td.Rows)
            { 
                string strsql = "SELECT Employee_ID FROM Employee_tbl WHERE Employee_ID = @Employee_ID";
                bool checkresult = await SqlDataAccess.Checkdata(strsql, new { Employee_ID = row["Employee_ID"] });

                // Checks if the employee ID is Not exist
                if (!checkresult)
                {
                    newRows.ImportRow(row);
                    Debug.WriteLine("Employee ID: " + row["Employee_ID"] + " is inserted");

                    var parameters = new
                    {
                        Employee_ID = row["Employee_ID"],
                        FullName = row["FullName"],
                        Process = row["Process"],
                        Affiliation = row["Affiliation"],
                        Department_ID = depid
                    };
                    await SqlDataAccess.UpdateInsertQuery("AddEmployee", parameters);
                }
            }
            return result;
        }
    }
}
