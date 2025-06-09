using Attendance_Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    public  interface IEmployee
    {
        Task<List<Employee>> GetEmployees();
        Task<List<Department>> GetDepartments();
        Task<bool> AddEmployee(Employee employee);
        Task<bool> UpdateEmployee(Employee employee, string emptemp);
        Task<bool> DeleteEmployee(string employee);
        Task<bool> UploadEmployee(DataTable td, int depid, int method);
    }
}
