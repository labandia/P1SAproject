using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    internal class EmployeeRespositoryV2 : CRUD_Repository<Employee>, IEmployee
    {
        public Task<bool> AddEmployee(Employee emp) => AddUpdateData("AddEmployee", emp);
        public Task<bool> DeleteEmployee(string empID) => DeleteData($@"
                UPDATE Employee_tbl SET IsDelete = 0 WHERE Employee_ID = @Employee_ID", 
                new { Employee_ID = empID });
  
        public Task<List<Employee>> GetEmployees() => GetDataList("ManageEmployee");

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
