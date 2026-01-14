
using Attendance_Monitoring.Utilities;
using Attendance_Monitoring.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data;
using System.Linq;

namespace Attendance_Monitoring.Models
{
    public class EmployeeRespository : IEmployee
    {
        public Task<List<Employee>> GetEmployees(string emp = "", int dep = 0, string search = "")
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            string strquery = @"
                SELECT DISTINCT 
                    Employee_ID, FullName, Process, Affiliation, Department_ID
                FROM Employee_tbl
                WHERE IsDelete = 1";

            if (!string.IsNullOrEmpty(emp))
            {
                strquery += " AND Employee_ID = @Employee_ID";
                parameters.Add("@Employee_ID", emp);
            }

            if (dep != 0)
            {
                strquery += " AND Department_ID = @Department_ID";
                parameters.Add("@Department_ID", dep);
            }

            if (!string.IsNullOrEmpty(search))
            {
                strquery += $@" AND (
                                @Search IS NULL
                                OR Employee_ID LIKE '%' + @Search + '%'
                                OR FullName LIKE '%' + @Search + '%'
                              )";
                parameters.Add("@Search", search);
            }

            strquery += " ORDER BY FullName ASC";

            return SqlDataAccess.GetData<Employee>(strquery, parameters);
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

        public Task<bool> UploadEmployee(List<Employee> emp, int depid, int method)
        {
            throw new NotImplementedException();
        }
    }
}
