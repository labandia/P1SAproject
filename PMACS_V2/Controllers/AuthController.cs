
using System.Threading.Tasks;
using System.Web;
using System;
using System.Linq;
using System.Web.Mvc;
using PMACS_V2.Interface;
using PMACS_V2.Helper;
using ProgramPartListWeb.Helper;
using PMACS_V2.Utilities.Security;
using System.Diagnostics;
using PMACS_V2.Models;


namespace PMACS_V2.Controllers
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

            // Check if user Exist
            if (user == null)
                return JsonPostError("Invalid credentials / Username Doesnt is Exist", 400, "VALIDATION_ERROR");
            // Verify the User password
            if (!PasswordHasher.VerifyPassword(user.Password, password))
                return JsonPostError("Invalid credentials / password is incorrect", 400, "VALIDATION_ERROR");

            // Get the Role name 
            string role = _auth.GetuserRolename(user.Role_ID);
            string fullname = user.Fullname;

            // Generate token for accesstoken and refreshtoken
            var accessToken = JWTAuthentication.GenerateAccessToken(fullname, role, user.User_ID);
            var refreshToken = _auth.GetRefreshToken(fullname, role, user.User_ID);

            var data = new { access_token = accessToken, refresh_token = refreshToken, fullname, role, user.User_ID };

            return JsonSuccess(data, "Login Success");   

        }



        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();

            if (Request.Cookies["jwt"] != null)
            {
                var cookie = new HttpCookie("jwt")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cookie);
            }

            if (Request.Cookies["refresh_token"] != null)
            {
                var refreshCookie = new HttpCookie("refresh_token")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(refreshCookie);
            }

            return RedirectToAction("Login", "Account");
        }



        [HttpPost]
        public ActionResult LogoutV2()
        {
            Session.Clear();
            Session.Abandon();
            return Json(new { success = true, message = "Logged out successfully" });
        }
    }
}