

using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface IAuthRepository
    {
        Task<List<AuthModel>> GetByUsername(string username, int proj);
        string GetRefreshToken(string fullname, string role, int userId);
        string GetuserRolename(int roleid);
        bool VerifyPassword(string enteredPassword, string storedHash);
        Task<bool> Changepassword(int ID, string newpass);
    }
}
