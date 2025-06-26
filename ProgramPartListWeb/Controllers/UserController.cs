using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<ActionResult> CreateUser()
        {
            // Roles 2 : for User register
            var dataobj = new
            {
                Username = Request.Form["usertext"],
                Password = PasswordHasher.Hashpassword(Request.Form["passtext"]),
                Role_ID = 2,
                First_Name = Request.Form["fname"],
                Last_Name = Request.Form["lname"]
            };
            var formdata = GlobalUtilities.GetMessageResponse(await _user.RegiserUserData(dataobj), 0);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CheckMatchPassword(int ID, string password)
        {     
            var datalist = await _user.GetUserById(ID);
            bool result = PasswordHasher.VerifyPassword(datalist.Password, password);
            return Json(result, JsonRequestBehavior.AllowGet);
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

        

        [HttpGet]
        public async Task<ActionResult> GetUserInformation(string accessToken)
        {
            try
            {
                var principal = JwtHelper.ValidateToken(accessToken);
                string userId = principal.FindFirst("UserId")?.Value;
                //var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var name = principal.FindFirst(ClaimTypes.Name)?.Value;
                var role = principal.FindFirst(ClaimTypes.Role)?.Value;

                var datalist = await _user.GetUserById(Convert.ToInt32(userId));
                //var data = datalist.FirstOrDefault(p => p.Employee_ID == empID);
                return Json(new { success = true, userId, name, role, datalist.Password }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
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




    }
}