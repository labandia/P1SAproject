using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    internal interface IAuthService
    {
        Task<List<AuthModel>> LoginCredentials(string user);
        Task<bool> UserRegistration(object parameters);
        Task<bool> ResetPassword(string email);
    }
}
