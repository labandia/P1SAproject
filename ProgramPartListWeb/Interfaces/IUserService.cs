using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    internal interface IUserService
    {
        Task<List<UsersModel>> GetAllUsersAsync();
    }
}
