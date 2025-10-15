using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Repository
{
    public class EmployeeRepository : IEmployee
    {
        public  async Task<List<EmployeeModel>> GetAllEmployee() => await SqlDataAccess.GetData<EmployeeModel>("EmployeeData");

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