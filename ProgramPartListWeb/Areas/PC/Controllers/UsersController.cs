using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProgramPartListWeb.Areas.PC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _user;

        public UsersController(IUserRepository user)
        {
            _user = user;
        }

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

                    SetFormsAuthentication(userRow.Role_ID);
                    FormsAuthentication.SetAuthCookie(strRole, false);
                    // Store user information in the session
                    Session["UserID"] = userRow.User_ID;
                    Session["Fullname"] = fullname;
                    Session["Role"] = strRole;

                    results.StatusCode = 200;
                    results.Message = "Login Successfully";
                    results.Data =  new { access_token = token, refresh_token = refreshToken };
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

            return Json("", JsonRequestBehavior.AllowGet);


        }


        [HttpGet]
        public async Task<ActionResult> GetUsersFullname(int userID) {
            string Fullname = await _user.UsersFullname(userID);
            return Json(new { Fullname }, JsonRequestBehavior.AllowGet);
        }



        public void SetFormsAuthentication(int userID)
        {
            // Set Forms Authentication ticket
            var authTicket = new FormsAuthenticationTicket(
                1,
                GlobalUtilities.UserRolesname(userID), // This becomes Identity.Name
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                userID.ToString() // user data - optional
            );

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(authCookie);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> RegisterUsers()
        {
            string hashpassword = PasswordHasher.Hashpassword(Request.Form["passtext"]);

            // Roles 2 : for User register
            var dataobj = new
            {
                Username = Request.Form["usertext"],
                Password = hashpassword,
                Role_ID = 2,
                First_Name = Request.Form["fname"],
                Last_Name = Request.Form["lname"]
            };

            bool result = await _user.RegiserUserData(dataobj);

            var formdata = GlobalUtilities.GetMessageResponse(result, 0);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> RefreshToken(string refreshToken)
        {
            try
            {
                var Users = await _user.GetAllusers();
                var principal = JwtHelper.ValidateToken(refreshToken, true);

                if (principal == null)
                {
                    return Json(new { success = false, message = "Invalid or expired refresh token" }, JsonRequestBehavior.AllowGet);
                }

                var userId = int.Parse(principal.FindFirst("UserId").Value);
                var user = Users.FirstOrDefault(u => u.User_ID == userId);

                if (user == null)
                {
                    return Json(new { success = false, message = "User not found" }, JsonRequestBehavior.AllowGet);
                }

                var newAccessToken = JwtHelper.GenerateAccessToken(user.Username, Convert.ToString(user.Role_ID), user.User_ID);
                return Json(new { success = true, accessToken = newAccessToken });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to refresh token: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}