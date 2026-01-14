using Attendance_Monitoring.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Attendance_Monitoring.Repositories
{
    public interface IEmployee
    {
        Task<List<Employee>> GetEmployees(string emp, int dep, string search);
        Task<bool> AddEmployee(Employee emp);
        Task<bool> UpdateEmployee(Employee emp, string temp);
        Task<bool> DeleteEmployee(string empID);
        Task<bool> UploadEmployee(List<Employee> emp, int depid, int method);
    }
}
