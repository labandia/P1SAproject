using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UsersModel>> GetAllusers();
        Task<bool> CheckUserTable(string employ);
        Task<bool> CheckAccountsTable(string employ);


        Task<UsersModel> GetUserById(int userId);

        Task<int> GetUserID(string Employee);

        Task<string> UsersFullname(int id);
        Task<bool> RegiserUserData(object parameters);

        Task<bool> CreateNewAccount(RegisterModel reg);
    }
}
