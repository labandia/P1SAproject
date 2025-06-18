using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ProductConfirm.Models
{
    public interface IUsers
    {
        Task<List<AuthModel>> LoginCredentials(string user);

        //Get the username info
        Task<List<Users>> Getusernameinfo(string users);
        Task<bool> CheckusersExist(string username);
        //Add a new user acccount
        Task<bool> RegisterUser(RegisterModel users);

    }
}
