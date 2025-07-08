
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities.Common;

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
            var user = (await _auth.GetByUsername(username, proj)).FirstOrDefault();
            var results = new DataMessageResponse<object> { };

            // Check If the user Exist
            if (user == null)
                return JsonPostError("Invalid credentials / Username Doesnt is Exist", 400, "VALIDATION_ERROR");
            // Check If the Password is Correct
            if (!PasswordHasher.VerifyPassword(user.Password, password))
                return JsonPostError("Invalid credentials / password is incorrect", 400, "VALIDATION_ERROR");

            string role = _auth.GetuserRolename(user.Role_ID);
            string fullname = user.Fullname;

            var accessToken = JwtHelper.GenerateAccessToken(fullname, role, user.User_ID);
            var refreshToken = _auth.GetRefreshToken(user.User_ID);

            var data = new { access_token = accessToken, refresh_token = refreshToken, fullname, role };

            return JsonSuccess(data, "Login Successfully");
        }


        [HttpPost]
        public async Task<ActionResult> ChangePasswordUser(int userID, string newpass)
        {
            string hashpassword = PasswordHasher.Hashpassword(newpass);
            bool result = await _auth.Changepassword(userID, hashpassword);
            var formdata = GlobalUtilities.GetMessageResponse(result, 1);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult>MatchPassword(string datapass, string currentpass)
        {
            await Task.Delay(100);
            bool result = _auth.VerifyPassword(currentpass, datapass);
            return Json(result);
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

        //[HttpPost]
        //public async Task<ActionResult>RefreshToken(string refreshToken)
        //{
        //    try
        //    {
        //        var Users = await _user.GetAllusers();
        //        var principal = JwtHelper.ValidateToken(refreshToken, true);

        //        if (principal == null)
        //        {
        //            return Json(new { success = false, message = "Invalid or expired refresh token" }, JsonRequestBehavior.AllowGet);
        //        }

        //        var userId = int.Parse(principal.FindFirst("UserId").Value);
        //        var user = Users.FirstOrDefault(u => u.User_ID == userId);

        //        if (user == null)
        //        {
        //            return Json(new { success = false, message = "User not found" }, JsonRequestBehavior.AllowGet);
        //        }

        //        var newAccessToken = JwtHelper.GenerateAccessToken(user.Username, Convert.ToString(user.Role_ID), user.User_ID);
        //        return Json(new { success = true, accessToken = newAccessToken });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = "Failed to refresh token: " + ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}


        [HttpPost]
        public ActionResult LogoutV2()
        {
            Session.Clear();
            Session.Abandon();
            return Json(new { success = true, message = "Logged out successfully" });
        }

       
    }
}