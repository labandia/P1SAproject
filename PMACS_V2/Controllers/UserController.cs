using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PMACS_V2.Helper;
using PMACS_V2.Interface;
using PMACS_V2.Models;
using PMACS_V2.Utilities.Security;

namespace PMACS_V2.Controllers
{
    public class UserController : ExtendController
    {
        private readonly IUserRepository _user;
        private readonly IEmployee _emp;

        public UserController(IUserRepository user, IEmployee emp)
        {
            _user = user;
            _emp = emp;
        }


        public class RefreshTokenModel
        {
            public string RefreshToken { get; set; }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CreateNewUser()
        {
            var roleIdValue = Request.Form["Roles_ID"];
            int roleId = string.IsNullOrEmpty(roleIdValue) ? 2 : Convert.ToInt32(roleIdValue);

            try
            {
                var obj = new RegisterModel
                {
                    Project_ID = Convert.ToInt32(Request.Form["ProjectID"]),
                    Username = Request.Form["RegUsername"],
                    Password = PasswordHasher.Hashpassword(Request.Form["RegPassword"]),
                    Email = Request.Form["Email"],
                    Role_ID = roleId,
                    FullName = Request.Form["FullName"],
                    Employee_ID = Request.Form["Employee_ID"]
                };

                // Checks if the user Exist 
                bool IsExist = await _user.CheckAccountsTable(obj);
                if (IsExist) return JsonError("Username is already created", 500);

                // Insert in the Database
                await _user.CreateNewAccount(obj);

                return JsonCreated(obj, "New Account Created Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(string CEmployee_ID, string CNewPassword)
        {
            var data = await _user.GetAllusers();
            int GetUserID = data.SingleOrDefault(res => res.Employee_ID == CEmployee_ID).User_ID;

            var obj = new ChangePassModel
            {
                User_ID = GetUserID,
                Project_ID = 9,
                Password = PasswordHasher.Hashpassword(CNewPassword)
            };

            bool result = await _user.Changepassword(obj);
            if (!result) return JsonValidationError();
            return JsonCreated(null, "Change Password Successfully");
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
        public async Task<ActionResult> CheckEmployeeID(string empID)
        {
            var data = await _user.GetAllusers();
            var filterData = data.Where(res => res.Employee_ID == empID);
            return Json(filterData.Any() ? true : false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RefreshToken(RefreshTokenModel request)
        {
            var principal = JWTAuthentication.ValidateRefreshToken(request.RefreshToken);

            if (principal == null)
                return Json(new { success = false, message = "Invalid refresh token" });


            string userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var fullname = principal.FindFirst("Fullname")?.Value;
            string role = principal.FindFirst(ClaimTypes.Role)?.Value;


            if (string.IsNullOrEmpty(userId))
                return Json(new { success = false, message = "Invalid token claims" });


            var newAccessToken = JWTAuthentication.GenerateAccessToken(fullname, role, int.Parse(userId));
            var newRefreshToken = JWTAuthentication.GenerateRefreshToken(fullname, role, int.Parse(userId)); // optional

            return Json(new
            {
                success = true,
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        [HttpGet]
        [JwtAuthorize]
        public async Task<ActionResult> GetUserInformation(string accessToken)
        {
            try
            {
                var identity = (ClaimsIdentity)User.Identity;

                var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var name = identity.FindFirst(ClaimTypes.Name)?.Value;
                var role = identity.FindFirst(ClaimTypes.Role)?.Value;

                var datalist = await _user.GetUserById(Convert.ToInt32(userId));

                return Json(new
                {
                    success = true,
                    userId,
                    name,
                    role,
                    datalist.Employee_ID,
                    datalist.Password,
                    datalist.Signature,
                    datalist.Email
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetUserV2()
        {
            var user = new
            {
                Id = 1,
                Name = "John Doe",
                Email = "john@email.com"
            };

            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index() { 
            return View();
        }
    }
}