using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UsersModel>> GetAllusers();
        Task<UsersModel> GetUserById(int userId);



        Task<string> UsersFullname(int id);
        Task<bool> RegiserUserData(object parameters);
        

    }
}
