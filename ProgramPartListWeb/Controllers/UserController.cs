using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProgramPartListWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _user;
        private readonly IEmployee _emp;

        public UserController(IUserRepository user, IEmployee emp)
        {
            _user = user;
            _emp = emp;
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
                    results.Data =  new { fullname = fullname, access_token = token, refresh_token = refreshToken};
                }
                else
                {
                    results.StatusCode = 401;
                    results.Message = "Invalid credentials / Password is incorrect";
                    results.Data = null;
                }
            }
            else
            {
                results.StatusCode = 401;
                results.Message = "Invalid credentials / Username doesnt exist";
                results.Data = null;
            }

            return Json(results, JsonRequestBehavior.AllowGet);


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
            //Debug.WriteLine(hashpassword);
            bool result = await _user.RegiserUserData(dataobj);

            var formdata = GlobalUtilities.GetMessageResponse(result, 0);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> SearchEmployeeID(string empID)
        {
            try
            {
                var datalist = await _emp.GetAllEmployee() ?? new List<EmployeeModel>();
                var data = datalist.FirstOrDefault(p => p.Employee_ID == empID);

                var formdata = GlobalUtilities.GetDataMessage(data);
                return Json(formdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to refresh token: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }




        [HttpPost]
        public async Task<ActionResult>RefreshToken(string refreshToken)
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

        [HttpGet]
        public async Task<ActionResult> GetUsersFullname(int userID)
        {
            string Fullname = await _user.UsersFullname(userID);
            return Json(new { Fullname }, JsonRequestBehavior.AllowGet);
        }

        //IDLE USER WITHIN 60 SECONDs
        [HttpGet]
        public JsonResult CheckSession()
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            return Json(new { isAuthenticated }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return Json(new { success = true, message = "Logged out successfully" });
        }

    }
}