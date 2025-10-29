
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities.Security;

namespace ProgramPartListWeb.Controllers
{
    public class AuthController : ExtendController
    {
        private readonly IAuthRepository _auth;
        public AuthController(IAuthRepository auth) => _auth = auth;


        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Authenticate(string username, string password, int proj = 1)
        {
            var user = (await _auth.GetByUsername(username.Trim(), proj)).FirstOrDefault();
            var results = new DataMessageResponse<object> { };

            // Check If the user Exist
            if (user == null)
                return JsonPostError("Invalid credentials / Username Doesnt is Exist", 400, "VALIDATION_ERROR");
            // Check If the Password is Correct
            if (!PasswordHasher.VerifyPassword(user.Password, password))
                return JsonPostError("Invalid credentials / password is incorrect", 400, "VALIDATION_ERROR");

            string role = _auth.GetuserRolename(user.Role_ID);
            string fullname = user.Fullname;

            var accessToken = JWTAuthentication.GenerateAccessToken(fullname, role, user.User_ID);
            var refreshToken = _auth.GetRefreshToken(fullname, role, user.User_ID);

            var data = new { access_token = accessToken, refresh_token = refreshToken, fullname, role, user.User_ID };

            return JsonSuccess(data, "Login Successfully");
        }
        [HttpPost]
        public async Task<ActionResult>MatchPassword(string datapass, string currentpass)
        {
            await Task.Delay(100);
            bool result = _auth.VerifyPassword(currentpass, datapass);
            return JsonCreated(result);
        }
       
    }
}