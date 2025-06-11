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
      
        public async Task<List<AuthModel>> LoginCredentials(string user)
        {
            string strquery = @"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                WHERE ua.Username  =@Username AND IsActive = 1";
            return await UsersAccess.UserGetData<AuthModel>(strquery, new { Username = user });
        }

        public async Task<List<AuthModelV2>> LoginCredentialsV2(string user, int proj)
        {
            string strquery = "SELECT  UA.User_ID, UA.Username, UA.Password, " +
                                "CONCAT(UA.First_Name, UA.Last_Name) as FullName, " +
                                "UA.ProfileImage, UP.Role_In_Project as Role_ID, " + 
	                            "RS.Role_Name, UA.Status " +
                            "FROM UserAccounts  UA " +
                            "INNER JOIN  UserProjects UP ON UP.User_ID = UA.User_ID " +
                            "INNER JOIN  SystemProjects SP ON UP.Project_ID = SP.Project_ID " +
                            "LEFT JOIN  UserRoles RS ON RS.Role_ID = UA.Role_ID " +
                            "WHERE SP.Project_ID =@Project_ID AND UA.Username =@Username AND UA.Status = 'Active' ";

            var parameters = new { Username = user, Project_ID = proj };

            return await UsersAccess.UserGetData<AuthModelV2>(strquery, parameters);
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

        public async Task<List<UsersModel>> GetAllusers()
        {
            string strquery = "SELECT ua.User_ID, ua.First_Name, ua.Last_Name, " +
                              "ua.Username, ua.Password, ua.Status " +
                              "FROM UserAccounts ua ";
            return await UsersAccess.UserGetData<UsersModel>(strquery);
        }

       
        public async Task<string> GetRolesByUserId(int userId)
        {
            string strquery = "SELECT Role_ID FROM UserAccounts WHERE User_ID =@User_ID";
            var parameters = new { User_ID = userId };
            int  roleid = await UsersAccess.GetUserCountData(strquery, parameters);
            return GlobalUtilities.UserRolesname(roleid);
        }
    }
}