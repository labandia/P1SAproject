using PMACS_V2.Models;
using PMACS_V2.Helper;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PMACS_V2.Interface;
using System.Diagnostics;

namespace PMACS_V2.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _user;

        public UserController(IUserRepository user)
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
                string strRole = userRow.Roles_ID == 2 ? "Users" : (userRow.Roles_ID == 1 ? "Admin" : "Leader");
                string fullname = userRow.Fullname;
                //// CHECKS THE PASSWORD IF IS CORRECT
                if (PasswordHasher.VerifyPassword(userRow.Password, password))
                {

                    // Generate the token
                    var token = JwtHelper.GenerateAccessToken(fullname, strRole, userRow.Account_ID);
                    var refreshToken = JwtHelper.GenerateRefreshToken(userRow.Account_ID);

                    // Store user information in the session
                    Session["UserID"] = userRow.Account_ID;
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

            return Json(results, JsonRequestBehavior.AllowGet);


        }
    }
}