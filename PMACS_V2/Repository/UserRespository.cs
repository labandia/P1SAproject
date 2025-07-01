using PMACS_V2.Interface;
using PMACS_V2.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMACS_V2.Utilities;
using System.Diagnostics;

namespace PMACS_V2.Repository
{
    public class UserRespository : IUserRepository
    {
        public Task<List<Users>> GetAllusers()
        {
            throw new NotImplementedException();
        }

        public async  Task<List<AuthModel>> LoginCredentials(string user)
        {
            string strquery = @"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                WHERE ua.Username  =@Username AND IsActive = 1";
            return await UsersAccess.UserGetData<AuthModel>(strquery, new { Username = user });
        }

        public Task<bool> RegiserUserData(object parameters)
        {
            throw new NotImplementedException();
        }

        public Task<string> UsersFullname(int id)
        {
            throw new NotImplementedException();
        }
    }
}