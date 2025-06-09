using PMACS_V2.Interface;
using PMACS_V2.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Repository
{
    public class UserRespository : IUserRepository
    {
        public Task<List<Users>> GetAllusers()
        {
            throw new NotImplementedException();
        }

        public async  Task<List<Users>> LoginCredentials(string user)
        {
            string strquery = "SELECT Account_ID,Fullname,Password,Roles_ID " +
                              "FROM Useraccount_tbl " +
                              "WHERE Fullname =@Fullname";
            var parameters = new { Fullname = user };

            return await SqlDataAccess.GetData<Users>(strquery, parameters);
        }

        public Task<bool> RegiserUserData(object parameters)
        {
            throw new NotImplementedException();
        }

        public Task<string> UsersFullname(int id)
        {
            throw new NotImplementedException();
        }
    }
}