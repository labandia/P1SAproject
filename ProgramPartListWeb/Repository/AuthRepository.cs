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

        public Task<List<AuthModel>> GetByUsername(string username, int proj)
        {
            string strquery = @"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                INNER JOIN ProjectList p ON p.Project_ID = ua.Project_ID
                                WHERE ua.IsActive = 1 AND ua.Project_ID = @Project_ID
                                AND (ua.Username = @Username OR  u.Employee_ID = @Username)";
            int projInt = (username == "Admin" || username == "24050006") ?  1 : proj;         
            return  UsersAccess.UserGetData<AuthModel>(strquery, new { Username = username, Project_ID  = projInt});
        }
        public string GetRefreshToken(int userId) => JwtHelper.GenerateRefreshToken(userId);
 
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

        public bool VerifyPassword(string enteredPassword, string storedHash) => PasswordHasher.VerifyPassword(storedHash, enteredPassword);
       
        public Task<bool> Changepassword(int ID, string newpass)
        {
            string strsql = $@"UPDATE UserAccounts SET Password =@Password
                               WHERE User_ID =@User_ID";
            var parameter = new { Password = newpass, User_ID = ID };
            return SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }
    }
}