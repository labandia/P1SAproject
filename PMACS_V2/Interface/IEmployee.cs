using PMACS_V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Interface
{
    public interface IEmployee
    {
        Task<List<EmployeeModel>> GetAllEmployee();
        Task ReceiveData(string Emp, string Fullname, string Affili, string process, int dep);
        Task receiveNotification(string message);
    }
}
