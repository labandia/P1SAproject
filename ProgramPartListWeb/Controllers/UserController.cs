using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProgramPartListWeb.Controllers
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CreateNewUser()
        {
            try
            {
                var obj = new RegisterModel
                {
                    Project_ID = Convert.ToInt32(Request.Form["ProjectID"]),
                    Username = Request.Form["RegUsername"],
                    Password = PasswordHasher.Hashpassword(Request.Form["RegPassword"]),
                    Role_ID = 2,
                    FullName = Request.Form["FullName"],
                    Employee_ID = Request.Form["Employee_ID"]
                };

                // Checks if the user Exist 
                bool IsExist = await _user.CheckAccountsTable(obj);
                if (IsExist) return JsonError("Username is already created", 500);
           
                // Insert in the Database
                await _user.CreateNewAccount(obj);

                return JsonCreated(obj, "Add Inspector Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
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
            var data = await _user.GetUserEmployeeID();
            var filterData = data.Where(res => res.Employee_ID == empID);
            return Json(filterData.Any() ? true : false, JsonRequestBehavior.AllowGet);
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



        [HttpPost]
        public async Task<ActionResult> UploadSignature(SignatureModel sig)
        {
            try
            {
                if (sig.SignatureImage != null && sig.SignatureImage.ContentLength > 0)
                {
                    //save file to the Database
                    string filename = Path.GetFileName(sig.SignatureImage.FileName);
                    var pathfile = Path.Combine(@"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures", filename);
                    // Ensure directory exists
                    var dir = Path.GetDirectoryName(pathfile);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    bool result = await _user.SaveSignatureData(sig.UserID, filename);

                    // if the Image file name is Save 
                    if (result)
                    {
                        sig.SignatureImage.SaveAs(pathfile);
                    }


                    return Json(new { success = true, message = "Upload Success" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Failed Upload" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult RefreshToken(string refreshToken)
        {
            var principal = JWTAuthentication.ValidateRefreshToken(refreshToken);

            if (principal == null)
            {
                return Json(new { success = false, message = "Invalid refresh token" });
            }

            var fullName = principal.Claims.FirstOrDefault(c => c.Type == "FullName")?.Value;
            var role = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userId = int.Parse(principal.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? "0");

            var newAccessToken = JWTAuthentication.GenerateAccessToken(fullName, role, userId);
            var newRefreshToken = JWTAuthentication.GenerateRefreshToken(userId);

            return Json(new
            {
                success = true,
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
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


        public ActionResult GetSignatureImage(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return HttpNotFound();

            string folderPath = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures\";
            string fullPath = Path.Combine(folderPath, filename);

            if (!System.IO.File.Exists(fullPath))
                return File("~/Content/Images/bussiness-man.png", "image/png");

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, "image/png"); // Change to "image/jpeg" if needed
        }

    }
}