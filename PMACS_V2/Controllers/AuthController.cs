
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

namespace PMACS_V2.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _user;

        public AuthController(IUserRepository user) => _user = user;

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Authenticate(string username, string password)
        {
            var data = await _user.LoginCredentials(username);
            var results = new DataMessageResponse<object> { };

            // CHECKS IF THE USERNAME EXIST
            if (data.Count() > 0)
            {
                // GET ONLY ONE ROW DATA
                var userRow = data.FirstOrDefault();
                string strRole = GlobalUtilities.UserRolesname(userRow.Role_ID);
                string fullname = userRow.Fullname;
                // CHECKS THE PASSWORD IF IS CORRECT
                if (PasswordHasher.VerifyPassword(userRow.Password, password))
                {
                    // Generate the token
                    var token = JwtHelper.GenerateAccessToken(fullname, strRole, userRow.User_ID);
                    var refreshToken = JwtHelper.GenerateRefreshToken(userRow.User_ID);

                    //SetFormsAuthentication(userRow.Role_ID);
                    //FormsAuthentication.SetAuthCookie(strRole, false);
                    // Store user information in the session
                    Session["UserID"] = userRow.User_ID;
                    Session["Fullname"] = fullname;
                    Session["Role"] = strRole;

                    results.StatusCode = 200;
                    results.Message = "Login Successfully";
                    results.Data =  new { fullname = fullname, access_token = token, refresh_token = refreshToken };
                }
                else
                {
                    results.StatusCode = 401;
                    results.Message = "Invalid credentials / password is incorrect";
                    results.Data = null;
                }
            }
            else
            {
                results.StatusCode = 401;
                results.Message = "Invalid credentials / username doesnt exist";
                results.Data = null;
            }

            return Json(results, JsonRequestBehavior.AllowGet);


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