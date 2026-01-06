using PMACS_V2.Helper;
using PMACS_V2.Interface;
using PMACS_V2.Models;
using PMACS_V2.Utilities;
using PMACS_V2.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public Task<List<AuthModel>> GetByUsername(string username, int proj)
        {
            string strquery = $@"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                INNER JOIN ProjectList p ON p.Project_ID = ua.Project_ID
                                WHERE ua.IsActive = 1 AND ua.Project_ID = @Project_ID
                                AND (ua.Username = @Username OR  u.Employee_ID = @Username)";
            int projInt = (username == "Admin" || username == "24050006") ? 1 : proj;
            return UsersAccess.UserGetData<AuthModel>(strquery, new { Username = username, Project_ID = projInt });
        }

        public string GetRefreshToken(string fullname, string role, int userId) => JWTAuthentication.GenerateRefreshToken(fullname, role, userId);

    
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
    }

}