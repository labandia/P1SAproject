using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface IEmployee
    {
        Task<List<EmployeeModel>> GetAllEmployee();
        Task ReceiveData(string Emp, string Fullname, string Affili, string process, int dep);
        Task receiveNotification(string message);
    }
}
