using PMACS_V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsersModel>> GetAllusers();
        Task<UsersModel> GetUserById(int userId);
        Task<List<AuthModel>> LoginCredentials(string user);
        Task<string> UsersFullname(int id);
        Task<bool> RegiserUserData(object parameters);
        Task<bool> CreateNewAccount(RegisterModel reg);
        Task<List<Employee>>GetEmployees(string emp);

        Task<bool> CheckAccountsTable(RegisterModel reg);
        Task<bool> Changepassword(ChangePassModel ch);
    }
}
