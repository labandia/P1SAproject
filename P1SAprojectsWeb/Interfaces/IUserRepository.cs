using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UsersModel>> GetAllusers();
        Task<UsersModel> GetUserById(int userId);


        Task<bool> CheckAccountsTable(RegisterModel reg);
        Task<bool> CreateNewAccount(RegisterModel reg);

        Task<bool> SaveSignatureData(int userID, string fileName);


        Task<bool> Changepassword(ChangePassModel ch);
    }
}
