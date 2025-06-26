

using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface IAuthRepository
    {
        Task<List<AuthModel>> GetByUsername(string username);
        string GetRefreshToken(int userId);
        string GetuserRolename(int roleid);
        bool VerifyPassword(string enteredPassword, string storedHash);
        Task<bool> Changepassword(int ID, string newpass);
    }
}
