using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Repository
{
    public sealed class AuthRepository : IAuthRepository
    {
       
        public async Task<List<AuthModel>> GetByUsername(string username)
        {
            string strquery = @"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                WHERE ua.Username  =@Username AND IsActive = 1";
            return await UsersAccess.UserGetData<AuthModel>(strquery, new { Username = username });
        }

        public string GetRefreshToken(int userId)
        {
           return JwtHelper.GenerateRefreshToken(userId);
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

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return PasswordHasher.VerifyPassword(storedHash, enteredPassword);
        }
        public async Task<bool> Changepassword(int ID, string newpass)
        {
            string strsql = $@"UPDATE UserAccounts SET Password =@Password
                               WHERE User_ID =@User_ID";
            var parameter = new { Password = newpass, User_ID = ID };
            return await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }
    }
}