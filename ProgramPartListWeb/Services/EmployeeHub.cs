using Microsoft.AspNet.SignalR;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Hub
{
    public class EmployeeHub : Hub<IEmployee>
    {
        public async Task SendProduct(string Emp, string Fullname, string Affili, string process, int dep)
        {
            await Clients.All.ReceiveData(Emp, Fullname, Affili, process, dep);
        }
    }
}