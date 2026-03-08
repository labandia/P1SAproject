using PMACS_V2.Helper;
using PMACS_V2.Interface;
using PMACS_V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Repository
{
    public class EmployeeRepository : IEmployee
    {
        public  async Task<List<EmployeeModel>> GetAllEmployee() => await SqlDataAccess.GetDataAsync<EmployeeModel>("EmployeeData");

        public Task ReceiveData(string Emp, string Fullname, string Affili, string process, int dep)
        {
            throw new System.NotImplementedException();
        }

        public Task receiveNotification(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}