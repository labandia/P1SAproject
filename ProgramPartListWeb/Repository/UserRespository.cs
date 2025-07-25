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
        public Task<List<UsersModel>> GetAllusers()
        {
            string strquery = $@"SELECT ua.User_ID, ua.Username, 
                                ua.Password, u.Fullname, ua.Role_ID, u.Signature, u.Email
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                INNER JOIN ProjectList p ON p.Project_ID = ua.Project_ID
                                WHERE IsActive = 1";
            return UsersAccess.UserGetData<UsersModel>(strquery);
        }

        public Task<bool> CheckUserTable(string employ)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckAccountsTable(RegisterModel reg)
        {
            string strUsers = "SELECT COUNT(*) FROM Users WHERE Employee_ID =@Employee_ID";
            bool CheckUser = await SqlDataAccess.Checkdata(strUsers, new { Employee_ID = reg.Employee_ID });

            // If the User doesnt exist in the Users Table
            if (!CheckUser)
            {
                string insUser = "INSERT INTO Users(Employee_ID, FullName, Email) VALUES(@Employee_ID, @FullName, @Email)";
                await SqlDataAccess.UpdateInsertQuery(insUser, reg);
            }

            int UserID = await GetUserID(reg.Employee_ID);

            if (UserID != 0)
            {
                // Check if user Exist in the database
                string strAccounts = $@"SELECT COUNT(*) 
                                        FROM UserAccounts 
                                        WHERE (User_ID = @User_ID AND Username = @Username) 
                                        AND Project_ID = @Project_ID";
                bool checkAccount = await SqlDataAccess.Checkdata(strAccounts, new { User_ID = UserID, Username = reg.Username, Project_ID = reg.Project_ID });
                return checkAccount;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> CreateNewAccount(RegisterModel reg)
        {
            int UserID = await GetUserID(reg.Employee_ID);
            if (UserID == 0) return false;

            // if user account doesnt Exist
            string insAccout = $@"INSERT INTO UserAccounts(User_ID, Project_ID, Username, Password, Role_ID) 
                                    VALUES(@User_ID, @Project_ID, @Username, @Password, @Role_ID)";
            return await SqlDataAccess.UpdateInsertQuery(insAccout, new
            {
                User_ID = UserID,
                Project_ID = reg.Project_ID,
                Username = reg.Username,
                Password = reg.Password,
                Role_ID = reg.Role_ID
            });
        }




        public async Task<string> UsersFullname(int id)
        {
            string strquery = $@"SELECT First_Name, Last_Name FROM UserAccounts WHERE User_ID = @userid";
            var data = await UsersAccess.UserGetData<AuthModel>(strquery, new { userid = id });
            var RowData = data.FirstOrDefault();

            return RowData.Fullname;
        }


        public Task<bool> RegiserUserData(object parameters)
        {
            string strquery = "INSERT UserAccounts(Username, Password, Role_ID, First_Name, " +
                              "Last_Name) VALUES (@Username, @Password, @Role_ID, @First_Name, @Last_Name)";
            return UsersAccess.UpdateUserData(strquery, parameters); ;
        }

       
        public async Task<UsersModel> GetUserById(int userId)
        {
            var data = await GetAllusers();
            var filterData = data.SingleOrDefault(res => res.User_ID == userId);    

            return filterData == null ? null : filterData;
        }

        

        public Task<int> GetUserID(string Employee)
        {
            string strsql = "SELECT User_ID FROM Users WHERE Employee_ID =@Employee_ID";
            return SqlDataAccess.GetCountDataSync(strsql, new { Employee_ID = Employee });
        }

        public Task<List<UserEmployee>> GetUserEmployeeID()
        {
            string strsql = "SELECT Employee_ID, FullName, Email FROM Users";
            return SqlDataAccess.GetData<UserEmployee>(strsql, null);
        }

        public Task<bool> CheckUserAccount(string employ, string username)
        {
            string strsql = "SELECT Employee_ID, FullName, Email FROM Users";
            return SqlDataAccess.Checkdata(strsql, null);
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
    }
}