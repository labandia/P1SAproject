using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PMACS_V2.Helper;
using PMACS_V2.Interface;
using PMACS_V2.Models;
using PMACS_V2.Utilities;

namespace PMACS_V2.Repository
{
    public class UserRespository : IUserRepository
    {
        public Task<bool> Changepassword(ChangePassModel ch)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckAccountsTable(RegisterModel reg)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateNewAccount(RegisterModel reg)
        {
            throw new NotImplementedException();
        }

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

        public Task<List<Employee>> GetEmployees(string emp)
        {
            return UsersAccess.UserGetData<Employee>($@"SELECT Employee_ID,FullName,Process
                                                      ,Affiliation,Department_ID
                                                    FROM Employee_tbl
                                                    WHERE IsDelete = 1 AND Employee_ID = @Employee_ID", new
                                                    {
                                                        Employee_ID = emp
                                                    });
        }

        public async Task<UsersModel> GetUserById(int userId)
        {
            var data = await GetAllusers();
            var filterData = data.SingleOrDefault(res => res.User_ID == userId);

            return filterData == null ? null : filterData;
        }

        public async  Task<List<AuthModel>> LoginCredentials(string user)
        {
            string strquery = @"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                WHERE ua.Username  =@Username AND IsActive = 1";
            return await UsersAccess.UserGetData<AuthModel>(strquery, new { Username = user });
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