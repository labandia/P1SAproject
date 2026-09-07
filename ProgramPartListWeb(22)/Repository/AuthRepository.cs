using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;
using ProgramPartListWeb.Utilities.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Repository
{
    public sealed class AuthRepository : IAuthRepository
    {

        public Task<List<AuthModel>> GetByUsername(string username, int proj)
        {
            string strquery = $@"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                INNER JOIN ProjectList p ON p.Project_ID = ua.Project_ID
                                WHERE ua.IsActive = 1 AND ua.Project_ID = @Project_ID
                                AND (ua.Username = @Username OR  u.Employee_ID = @Username)";
            int projInt = (username == "Admin" || username == "24050006") ?  1 : proj;         
            return  UsersAccess.UserGetData<AuthModel>(strquery, new { Username = username, Project_ID  = projInt});
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

        public async Task<bool> UpdatesUserClienfoToDatabase(ClientsInfoModel client)
        {
            // Check if this ComputerName + IpAddress combination already exists
            const string checkSql = @"
                    SELECT COUNT(1)
                    FROM UsersInfoAccount
                    WHERE ComputerName = @ComputerName
                      AND IpAddress = @IpAddress;";

            bool exists = await SqlDataAccess.ExistsAsync(checkSql, new
            {
                client.ComputerName,
                client.IpAddress
            });

            // Already tracked — do nothing, per the "don't insert if exists" rule
            if (exists)
            {
                return false;
            }

            const string insertSql = @"
                INSERT INTO UsersInfoAccount (ComputerName, IpAddress, AccountName, Email)
                VALUES (@ComputerName, @IpAddress, @AccountName, @Email);";

            int rowsAffected = await SqlDataAccess.ExecuteAsync(insertSql, new
            {
                client.ComputerName,
                client.IpAddress,
                client.AccountName,
                client.Email
            });

            return rowsAffected > 0;
        }

        public bool VerifyPassword(string enteredPassword, string storedHash) => PasswordHasher.VerifyPassword(storedHash, enteredPassword);
       
        
    }
}