using PMACS_V2.Models;
using PMACS_V2.Helper;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PMACS_V2.Interface;
using ProgramPartListWeb.Helper;
using System.Web.Security;
using System.Web;
using System;
using System.Diagnostics;

namespace PMACS_V2.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _user;

        public UserController(IUserRepository user) => _user = user;
     

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


    }
}