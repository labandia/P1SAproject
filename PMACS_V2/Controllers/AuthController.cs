
using System.Threading.Tasks;
using System.Web;
using System;
using System.Linq;
using System.Web.Mvc;
using PMACS_V2.Interface;
using PMACS_V2.Models;
using PMACS_V2.Helper;
using System.Diagnostics;
using ProgramPartListWeb.Helper;
using System.Web.Security;
using System.IO;
using Microsoft.Ajax.Utilities;
using System.Data;

namespace PMACS_V2.Controllers
{
    public class AuthController : ExtendController
    {
        private readonly IUserRepository _user;

        public AuthController(IUserRepository user) => _user = user;

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Authenticate(string username, string password)
        {
            var user = (await _user.LoginCredentials(username)).FirstOrDefault();
            // Check if user Exist
            if (user == null)
                return JsonPostError("Invalid credentials / Username Doesnt is Exist", 400, "VALIDATION_ERROR");
            // Verify the User password
            if (!PasswordHasher.VerifyPassword(user.Password, password))
                return JsonPostError("Invalid credentials / password is incorrect", 400, "VALIDATION_ERROR");

            // Get the Role name 
            string role = GlobalUtilities.UserRolesname(user.Role_ID);
            string fullname = user.Fullname;

            // Generate token for accesstoken and refreshtoken
            var accessToken = JwtHelper.GenerateAccessToken(fullname, role, user.User_ID);
            var refreshToken = JwtHelper.GenerateRefreshToken(user.User_ID);

            var data = new { access_token = accessToken, refresh_token = refreshToken, fullname, role };

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