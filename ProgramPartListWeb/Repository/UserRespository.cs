using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Utilities;
using System.Net.PeerToPeer;
using System;

namespace ProgramPartListWeb.Data
{
    public class UserRespository : IUserRepository
    {
        public async Task<List<UsersModel>> GetAllusers()
        {
            string strquery = $@"SELECT ua.User_ID, ua.Username, 
                                ua.Password, u.Fullname, ua.Role_ID
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                INNER JOIN ProjectList p ON p.Project_ID = ua.Project_ID
                                WHERE IsActive = 1";
            return await UsersAccess.UserGetData<UsersModel>(strquery);
        }

        public Task<bool> CheckUserTable(string employ)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckAccountsTable(string employ)
        {
            throw new NotImplementedException();
        }


        public async Task<string> UsersFullname(int id)
        {
            string strquery = "SELECT First_Name, Last_Name " +
                               "FROM UserAccounts WHERE User_ID = @userid ";

            var parameters = new { userid = id };

            var data = await UsersAccess.UserGetData<AuthModel>(strquery, parameters);

            var RowData = data.FirstOrDefault();

            return RowData.Fullname;
        }


        public async Task<bool> RegiserUserData(object parameters)
        {
            string strquery = "INSERT UserAccounts(Username, Password, Role_ID, First_Name, " +
                              "Last_Name) VALUES (@Username, @Password, @Role_ID, @First_Name, @Last_Name)";
            bool result = await UsersAccess.UpdateUserData(strquery, parameters);
            return result;
        }

       
        public async Task<UsersModel> GetUserById(int userId)
        {
            var data = await GetAllusers();
            var filterData = data.SingleOrDefault(res => res.User_ID == userId);    

            return filterData == null ? null : filterData;
        }

        public async Task<bool> CreateNewAccount(RegisterModel reg)
        {
            string strUsers = "SELECT COUNT(*) FROM Users WHERE Employee_ID =@Employee_ID";
            bool CheckUser = await SqlDataAccess.Checkdata(strUsers, new { Employee_ID = reg.Employee_ID });
            bool result = false;

            // If the User doesnt exist in the Users Table
            if (!CheckUser)
            {
                string insUser = "INSERT INTO Users(Employee_ID, FullName, Email) VALUES(@Employee_ID, @FullName, @Email)";
                await SqlDataAccess.UpdateInsertQuery(insUser, reg);
            }

            int UserID = await GetUserID(reg.Employee_ID);
            if(UserID != 0)
            {
                // Check if user Exist in the database
                string strAccounts = "SELECT COUNT(*) FROM UserAccounts WHERE User_ID =@User_ID AND Project_ID =@Project_ID";
                bool checkAccount = await SqlDataAccess.Checkdata(strAccounts, new { User_ID = UserID, Project_ID = reg.Project_ID });

                if (!checkAccount)
                {
                    // if user account doesnt Exist
                    string insAccout = $@"INSERT INTO UserAccounts(User_ID, Project_ID, Username, Password, Role_ID) 
                                    VALUES(@User_ID, @Project_ID, @Username, @Password, @Role_ID)";
                    await SqlDataAccess.UpdateInsertQuery(insAccout, new
                    {
                        User_ID = UserID,
                        Project_ID = reg.Project_ID,
                        Username = reg.Username,
                        Password = reg.Password,
                        Role_ID = reg.Role_ID
                    });
                    result = true;
                }
            }
            else
            {
                throw new Exception("User ID not found.");
            }

           
            return result;
        }

        public Task<int> GetUserID(string Employee)
        {
            string strsql = "SELECT User_ID FROM Users WHERE Employee_ID =@Employee_ID";
            return SqlDataAccess.GetCountDataSync(strsql, new { Employee_ID = Employee });
        }

        
    }
}