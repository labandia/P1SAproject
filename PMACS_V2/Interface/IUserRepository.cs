using PMACS_V2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Interface
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllusers();
        Task<List<AuthModel>> LoginCredentials(string user);
        Task<string> UsersFullname(int id);
        Task<bool> RegiserUserData(object parameters);

    }
}
