using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UsersModel>> GetAllusers();

        Task<List<AuthModel>> LoginCredentials(string user);
        Task<List<AuthModelV2>> LoginCredentialsV2(string user, int proj);
        Task<string> UsersFullname(int id);
        Task<bool> RegiserUserData(object parameters);
        

    }
}
