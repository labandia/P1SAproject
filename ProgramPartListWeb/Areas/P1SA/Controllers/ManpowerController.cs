using DocumentFormat.OpenXml.EMMA;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Areas.P1SA.Interface;
using ProgramPartListWeb.Areas.P1SA.Models;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities.Security;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.P1SA.Controllers
{
    public class ManpowerController : ExtendController
    {
        private readonly IP1SAEmployeeRepository _emp;

        public ManpowerController(IP1SAEmployeeRepository emp)
        {
            _emp = emp;
        }

        //-----------------------------------------------------------------------------------------
        //---------------------------- USERS LOGIN   -------------------------------------====-----
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> Authenticate(string username, string password, int dept)
        {
            // Get the Users Information
            var user = _emp.Userslogin(username, dept);

            //// Check If the user Exist
            //if (user == null)
            //    return JsonPostError("Invalid credentials / Username Doesn't is Exist", 400, "VALIDATION_ERROR");
            //// Check If the Password is Correct
            //if (!PasswordHasher.VerifyPassword(user.PasswordHash, password))
            //    return JsonPostError("Invalid credentials / password is incorrect", 400, "VALIDATION_ERROR");

            //string role = _auth.GetuserRolename(user.Role_ID);
            //string fullname = user.Fullname;

            //var accessToken = JWTAuthentication.GenerateAccessToken(fullname, role, user.User_ID);
            //var refreshToken = _auth.GetRefreshToken(fullname, role, user.User_ID);

            //var data = new { access_token = accessToken, refresh_token = refreshToken, fullname, role, user.User_ID };

            return JsonSuccess("", "Login Successfully");
        }


        //-----------------------------------------------------------------------------------------
        //---------------------------- OVERALL SUMMARY   ------------------------------------------
        //-----------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------
        //---------------------------- P1SA EMPLOYEEMANAGEMENT ------------------------------------
        //-----------------------------------------------------------------------------------------
        [HttpGet]   
        public async Task<ActionResult> GetEmployeelist(
                    string search,
                    int depid = 0,
                    int gender = 0,
                    int pos = 0,
                    int agency = 0,
                    int status = 0,
                    int role = 0,
                    int page = 1,
                    int pageSize = 10)
        {
            var data = await _emp.GetEmployees(search, depid, gender, pos, agency, status, page, pageSize);

            //var data = role == 0 
            //        ? await _emp.GetEmployees(search, depid, gender,pos, agency, status, page, pageSize)
            //        : await _emp.GetProductionEmployees(search, gender, agency, status, page, pageSize);

            //int totalCount = await _emp.GetActualCountEmployee(depid, agency, status, gender);


            if (data == null)
                JsonNotFound("No Data found");

            var finaldata = new
            {
                payload = data,
                Total = 1
            };

            return JsonSuccess(finaldata, "Retrieved data successfully");
        }
       

        // Get the Employees Details    
        [HttpGet]
        public async Task<ActionResult> GetEmployeeDetails(int employeeID)
        {
            var data = await _emp.GetEmployees(employeeID);

            if (data == null)
                JsonNotFound("No Data found");

            return JsonSuccess(data, "Retrieved data successfully");
        }
        [HttpGet]
        public ActionResult DisplaytheImage(string filename)
        {

            if (string.IsNullOrWhiteSpace(filename))
                return HttpNotFound();

            string folderPath =
                @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Manpower\Molding\";

            string fullPath = Path.Combine(folderPath, filename);


            if (!System.IO.File.Exists(fullPath))
            {
                Debug.WriteLine("IMAGE NOT FOUND");

                return File(
                    Server.MapPath("~/Content/Images/no-image.png"),
                    "image/png"
                );
            }

            string extension = Path.GetExtension(fullPath).ToLowerInvariant();

            string contentType;

            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    contentType = "image/jpeg";
                    break;

                case ".png":
                    contentType = "image/png";
                    break;

                case ".gif":
                    contentType = "image/gif";
                    break;

                case ".webp":
                    contentType = "image/webp";
                    break;

                default:
                    contentType = "application/octet-stream";
                    break;
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);

            return File(fileBytes, contentType);
        }


        [HttpPost]
        public async Task<ActionResult> AddEmployeeList(P1SAEmployeesInputModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.EmployeeCode) || string.IsNullOrWhiteSpace(model.FullName))
            {
                return Json(new { Success = false, Message = "Employee Code and Full Name are required." });
            }

            try
            {
                model.IsDeleted = false;
                model.CreatedAt = DateTime.Now;

                var getinfo = await _emp.InsertEmployeeAsync(model);

                return JsonCreated(new { EmployeeId = getinfo.id,  EmployeeCode = getinfo.code }, "Add Employee Records Successfully");
            }
            catch (Exception ex)
            {
                // TODO: swap for your logging call — kept explicit rather than swallowed silently
                return Json(new { Success = false, Message = "Failed to save employee." });
            }

            //bool result = await _emp.AddAsync(model);

            //if (!result) JsonValidationError("Input Validation error");

            //return JsonCreated(model, "Add Employee Records Successfully");
        }


        [HttpPost]
        public async Task<ActionResult> UploadEmployeePhoto(int EmployeeId, string EmployeeCode, HttpPostedFileBase photo)
        {
            if (photo == null || photo.ContentLength == 0)
            {
                return Json(new { Success = false, Message = "No file received." });
            }

            const string basePath = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Manpower\Molding";

            var ext = Path.GetExtension(photo.FileName);
            var fileName = $"{EmployeeCode}{ext}";
            var savePath = Path.Combine(basePath, fileName);

            try
            {
           
                photo.SaveAs(savePath);
            }
            catch (UnauthorizedAccessException)
            {
                return Json(new { Success = false, Message = "No write access to the image share." });
            }
            catch (DirectoryNotFoundException)
            {
                return Json(new { Success = false, Message = "Image share path not found or unreachable." });
            }

            // Only the filename is stored — DisplaytheImage presumably prepends the same base path when reading it back
            bool imageresult = await _emp.AddEmployeeImageFileName(EmployeeId, fileName);

            return Json(new { Success = imageresult });
        }



        //[HttpPost]
        //public async Task<ActionResult> EditEmployeeList(P1SAEmployeesInputModel mode)
        //{
        //    bool result = await _emp.UpdateEmployee(mode);

        //    if (!result) JsonValidationError("Input Validation error");

        //    return JsonCreated(mode, "Data Modified Successfully");
        //}

        [HttpPost]
        public async Task<ActionResult> EditEmployeeDetails(P1SAEmployeesInputModel model)
        {
            Debug.Write($@"
                ID : {model.EmployeeId}
                Full name : {model.FullName}");
     

            try
            {
                // UpdatedAt is set server-side, not trusted from the client payload
                model.UpdatedAt = DateTime.Now;

                bool result = await _emp.UpdateAsync(model);

                if (!result) JsonValidationError("Input Validation error");

                return JsonCreated(model, "Data Modified Successfully");
            }
            catch (Exception ex)
            {
                // Log the real exception server-side; keep the client message generic
                // _logger.LogError(ex, "UpdateEmployee failed for EmployeeId {Id}", model.EmployeeId);
                return Json(new { Success = false, Message = "An error occurred while saving. Please try again." });
            }
        }

        //-----------------------------------------------------------------------------------------
        //---------------------------- P1SA ATTENDANCE SUMMARY ------------------------------------
        //-----------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult> GetAttendanceSummaryList(
                    string search,
                    int depid = 0,
                    int shift = -1)
        {
            var data = await _emp.GetAttendanceSummary(search, depid, shift);
            return JsonSuccess(data, "Retrieved data successfully");
        }


        // GET: P1SA/Manpower/NewlyHiredpage
        public ActionResult Dashboard() => View();

        // GET: P1SA/Manpower/NewlyHiredpage
        public ActionResult NewlyHiredpage() => View();
        // GET: P1SA/Manpower/AddNewEmployee
        public ActionResult AddNewEmployee() => View();

        // GET: P1SA/Manpower/EmployeeDetails/24050006
        public ActionResult EmployeeDetails(int EmployeeId) => View();

        // GET: P1SA/Manpower/EmployeeDetails/24050006
        public ActionResult EmployeeProductionDetails(int EmployeeId, int role) => View();
        // GET: P1SA/Manpower/ManageEmployee
        public ActionResult ManageEmployee() => View();

        // GET: P1SA/Manpower/Productions
        public ActionResult Productions() => View();

        // GET: P1SA/LoginPage
        public ActionResult LoginPage() => View();

        // GET: P1SA/Selection
        public ActionResult Index() => View();

        public ActionResult AttendanceSummary() => View();
        public ActionResult ManageAbsence() => View();

        public ActionResult CrossTrainee() => View();
    }
}