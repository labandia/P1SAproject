using SanyoDenki.Interfaces;
using SanyoDenki.Models;
using SanyoDenki.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SanyoDenki.Repository
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