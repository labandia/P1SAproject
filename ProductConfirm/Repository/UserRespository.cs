
using ProductConfirm.Models;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductConfirm.DataAccess
{
    public class UserRespository : IUsers
    {
        public async Task<bool> CheckusersExist(string users)
        {
            string strquery = "Prod_userlogin";
            var parameters = new { username = users };

            // Calls the data provider to get user information
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
