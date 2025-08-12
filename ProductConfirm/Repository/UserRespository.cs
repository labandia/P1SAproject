
using ProductConfirm.Models;
using ProductConfirm.Utilities;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductConfirm.DataAccess
{
    public class UserRespository : IUsers
    {
        public async Task<List<AuthModel>> LoginCredentials(string user)
        {
            string strquery = @"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                WHERE ua.Username  =@Username AND IsActive = 1";
            return await UsersAccess.UserGetData<AuthModel>(strquery, new { Username = user });
        }

        public async Task<bool> CheckusersExist(string users)
        {
            string strquery = "Prod_userlogin";
            var parameters = new { username = users };

            return await SqlDataAccess.Checkdata(strquery, parameters);
        }

      
        public async Task<List<Users>> Getusernameinfo(string users)
        {
            string strquery = "Prod_userlogin";
            var parameters = new { username = users };

            // Calls the data provider to get user information
            return await SqlDataAccess.GetData<Users>(strquery, parameters);
        }

        public async Task<bool> RegisterUser(RegisterModel users)
        {
            string strquery = "Prod_userRegister";
            var parameters = new { username = users.username, password = users.password, role_type = users.role_type, 
                                   Fname = users.Fname, Lname = users.lname, Project_ID = users.Project_ID};
            return await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }
    }
}
