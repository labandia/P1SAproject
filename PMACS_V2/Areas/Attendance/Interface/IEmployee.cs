using PMACS_V2.Areas.Attendance.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.Attendance.Interface
{
    public interface IEmployee
    {
        Task<List<Employee>> GetEmployees();
        Task<List<Department>> GetDepartments();
        Task<bool> AddEmployee(Employee employee);
        Task<bool> UpdateEmployee(Employee employee, string emptemp);
        Task<bool> DeleteEmployee(string employee);
        Task<bool> UploadEmployee(DataTable td, int depid, int method);
    }
}
