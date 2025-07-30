
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System;
using System.Threading.Tasks;
using System.Web;
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
        [RateLimiting(5, 1)] // Limits the No of Request
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
            var refreshToken = _auth.GetRefreshToken(user.User_ID);

            var data = new { access_token = accessToken, refresh_token = refreshToken, fullname, role };

            return JsonSuccess(data, "Login Successfully");
        }


        [HttpPost]
        public async Task<ActionResult> ChangePasswordUser(int userID, string newpass)
        {
            string hashpassword = PasswordHasher.Hashpassword(newpass);
            bool result = await _auth.Changepassword(userID, hashpassword);

            if (!result) return JsonValidationError();

            return JsonCreated(null, "Change Password Successfully");
        }

        [HttpPost]
        public async Task<ActionResult>MatchPassword(string datapass, string currentpass)
        {
            await Task.Delay(100);
            bool result = _auth.VerifyPassword(currentpass, datapass);
            return JsonCreated(result);
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