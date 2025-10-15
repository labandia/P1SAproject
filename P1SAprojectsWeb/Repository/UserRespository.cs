using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Utilities;
using System;
using System.IO;
using System.Diagnostics;

namespace ProgramPartListWeb.Data
{
    public class UserRespository : IUserRepository
    {
        public async Task<IEnumerable<UsersModel>> GetAllusers()
        {
            string strquery = $@"SELECT ua.User_ID, ua.Username, u.Employee_ID,
                                ua.Password, u.Fullname, ua.Role_ID, u.Signature, u.Email,
                                p.Project_ID, ua.IsActive
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                INNER JOIN ProjectList p ON p.Project_ID = ua.Project_ID
                                WHERE IsActive = 1";
            return await UsersAccess.UserGetData<UsersModel>(strquery);
        }

        public async Task<IEnumerable<UsersModel>> GetUserDatalist()
        {

            string strquery = $@"SELECT ua.User_ID, ua.Username, u.Employee_ID,
                                ua.Password, u.Fullname, ua.Role_ID, u.Signature, u.Email,
                                p.Project_ID, ua.IsActive
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                INNER JOIN ProjectList p ON p.Project_ID = ua.Project_ID
                                WHERE IsActive = 1";
            return await UsersAccess.UserGetData<UsersModel>(strquery);
        }

        public async Task<UsersModel> GetUserById(int userId)
        {
            var data = await GetAllusers();
            var filterData = data.SingleOrDefault(res => res.User_ID == userId);

            return filterData == null ? null : filterData;
        }
        public async Task<bool> CheckAccountsTable(RegisterModel reg)
        {
            try
            {
                // Step 1: Ensure user exists
                string strUsers = "SELECT COUNT(*) FROM Users WHERE Employee_ID =@Employee_ID";
                bool userExists = await SqlDataAccess.Checkdata(strUsers, new { Employee_ID = reg.Employee_ID });
                // If the User doesnt exist in the Users Table
                if (!userExists)
                {
                    // INSERT a new Employee ID 
                    string insUser = @"
                        INSERT INTO Users (Employee_ID, FullName, Email)
                        SELECT @Employee_ID, @FullName, @Email
                        WHERE NOT EXISTS (
                            SELECT 1 FROM Users WHERE Employee_ID = @Employee_ID
                        )";
                    await SqlDataAccess.UpdateInsertQuery(insUser, reg);
                }

                // Step 2: Get User ID
                string strUserId = "SELECT User_ID FROM Users WHERE Employee_ID = @Employee_ID";
                int? userId = await SqlDataAccess.GetCountData(strUserId, new { reg.Employee_ID });
                if (userId == null) return false;


                // Optional Query if does have a Email 
                string Updateinfor = @"UPDATE Users SET Email =@Email WHERE User_ID = @User_ID";
                await SqlDataAccess.UpdateInsertQuery(Updateinfor, new { User_ID = userId, Email = reg.Email });


                // Check if user Exist in the database
                string strAccounts = $@"SELECT COUNT(*) 
                                        FROM UserAccounts 
                                        WHERE (User_ID = @User_ID AND Project_ID = @Project_ID)";
                bool checkAccount = await SqlDataAccess.Checkdata(strAccounts, new { User_ID = userId.Value, Project_ID = reg.Project_ID });
                return checkAccount;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error : " + ex.Message);
                return false;
            }
        }
        public async Task<bool> CreateNewAccount(RegisterModel reg)
        {
            try
            {
                string strUserId = "SELECT User_ID FROM Users WHERE Employee_ID = @Employee_ID";
                int? userId = await SqlDataAccess.GetCountData(strUserId, new { reg.Employee_ID });
                if (userId == 0) return false;

                // if user account doesnt Exist
                string insAccout = $@"INSERT INTO UserAccounts(User_ID, Project_ID, Username, Password, Role_ID) 
                                    VALUES(@User_ID, @Project_ID, @Username, @Password, @Role_ID)";
                return await SqlDataAccess.UpdateInsertQuery(insAccout, new
                {
                    User_ID = userId,
                    Project_ID = reg.Project_ID,
                    Username = reg.Username,
                    Password = reg.Password,
                    Role_ID = reg.Role_ID
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
                return false;
            }
        }
        public async Task<bool> SaveSignatureData(int userID, string fileName)
        {
            string strsql = "UPDATE Users Set Signature =@Signature WHERE User_ID =@User_ID";

            var userData = await GetAllusers();
            string existingSignatureFile = userData.Where(res => res.User_ID == userID)
                                .Select(p => p.Signature)
                                .FirstOrDefault();
            if (!string.IsNullOrEmpty(existingSignatureFile))
            {
                var pathfile = Path.Combine(@"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures", existingSignatureFile);

                try
                {
                    if (File.Exists(pathfile)) File.Delete(pathfile);
                }
                catch (Exception ex)
                {
                    // Optional: log or handle exception
                    Debug.WriteLine($"Failed to delete signature file: {ex.Message}");
                }
            }


            return await SqlDataAccess.UpdateInsertQuery(strsql, new { Signature  = fileName, User_ID = userID});
        }
        public Task<bool> Changepassword(ChangePassModel ch)
        {
            string strsql = $@"UPDATE UserAccounts SET Password =@Password
                               WHERE User_ID =@User_ID AND Project_ID = 9";
            var parameter = new { Password = ch.Password, User_ID = ch.User_ID };
            return SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }
    }
}