using PMACS_V2.Interface;
using PMACS_V2.Models;
using PMACS_V2.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public async Task<List<AuthModel>> GetByUsername(string username)
        {
            string strquery = @"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                WHERE ua.Username  =@Username AND IsActive = 1";
            return await UsersAccess.UserGetData<AuthModel>(strquery, new { Username = username }, "Loginuser");
        }

        public string GetRefreshToken(int userId)
        {
            throw new NotImplementedException();
        }

        public string GetuserRolename(int roleid)
        {
            switch (roleid)
            {
                case 1:
                    return "SuperAdmin";
                case 2:
                    return "Admin";
                case 3:
                    return "Manager";
                case 4:
                    return "Supervisor";
                case 5:
                    return "Leader";
                default:
                    return "Users";
            }
        }

        public void RevokeRefreshToken(int userId)
        {
            throw new NotImplementedException();
        }
    }

}