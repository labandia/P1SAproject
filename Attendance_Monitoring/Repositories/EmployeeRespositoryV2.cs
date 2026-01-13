using Attendance_Monitoring.Models;
using Attendance_Monitoring.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    internal class EmployeeRespositoryV2 : CRUD_Repository<Employee>, IEmployee
    {
        public Task<bool> AddEmployee(Employee emp) => AddUpdateData("AddEmployee", emp);
        public Task<bool> DeleteEmployee(string empID) => DeleteData($@"
                UPDATE Employee_tbl SET IsDelete = 0 WHERE Employee_ID = @Employee_ID", 
                new { Employee_ID = empID });
  

        public Task<List<Employee>> GetEmployees(string emp, int dep)
        {
            List<string> conditions = new List<string>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(emp))
            {
                conditions.Add("Employee_ID = @Employee_ID");
                parameters.Add("@Employee_ID", emp);
            }

            if (dep != 0)
            {
                conditions.Add("Department_ID = @Department_ID");
                parameters.Add("@Department_ID", dep);
            }

            string whereClause = conditions.Any()
                ? "WHERE " + string.Join(" AND ", conditions)
                : string.Empty;

            string strquery = $@"
                    SELECT DISTINCT 
                        Employee_ID, FullName, Process, Affiliation, Department_ID
                    FROM Employee_tbl
                    {whereClause}
                    AND IsDelete = 1
                    ORDER BY FullName ASC";

            return SqlDataAccess.GetData<Employee>(strquery, parameters);
        }

        public Task<bool> UpdateEmployee(Employee emp, string temp)
        {
            var parameters = new
            {
                CurrentID = temp,
                NewID = emp.Employee_ID,
                FullName = emp.Fullname,
                Process = emp.Process,
                Affiliation = emp.Affiliation,
                Department_ID = emp.Department_ID
            };
            return AddUpdateData("UpdateEmployee", parameters);
        }

        public Task<bool> UploadEmployee(List<Employee> emp, int depid, int method)
        {
            throw new NotImplementedException();
        }
    }
}
