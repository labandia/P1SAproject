using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Services
{
    public class AuthService : IAuthService
    {
        public Task<List<AuthModel>> LoginCredentials(string user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserRegistration(object parameters)
        {
            throw new NotImplementedException();
        }
    }
}