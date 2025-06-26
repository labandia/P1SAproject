using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Utilities;

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
    }
}