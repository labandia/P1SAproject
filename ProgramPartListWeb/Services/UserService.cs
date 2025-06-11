using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Services
{
    public class UserService : IUserService
    {
        public Task<List<UsersModel>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}