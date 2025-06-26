using PMACS_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMACS_V2.Interface
{
    public interface IAuthRepository
    {
        Task<List<AuthModel>> GetByUsername(string username);
        string GetRefreshToken(int userId);
        void RevokeRefreshToken(int userId);

        string GetuserRolename(int roleid);
    }
}
