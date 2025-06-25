using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace ProgramPartListWeb.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _user;
        private readonly IEmployee _emp;
            
        public AuthService(IUserRepository user, IEmployee emp)
        {
            _user = user;
            _emp = emp;
        }

        public async Task<object> LoginCredentials(string user, string pass)
        {
            string strquery = @"SELECT ua.User_ID, ua.Username, ua.Password, ua.Role_ID, u.Fullname
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                WHERE ua.Username  =@Username AND IsActive = 1";
            var data = await UsersAccess.UserGetData<AuthModel>(strquery, new { Username = user });
            var results = new DataMessageResponse<object> { };


            // CHECKS IF THE USERNAME EXIST
            if (data == null || !data.Any())
            {
                // GET ONLY ONE ROW DATA
                var userRow = data.FirstOrDefault();
                string strRole = GlobalUtilities.UserRolesname(userRow.Role_ID);

                // CHECKS THE PASSWORD IF IS CORRECT
                if (PasswordHasher.VerifyPassword(userRow.Password, pass))
                {
                    // Generate the token
                    var token = JwtHelper.GenerateAccessToken(userRow.Fullname, strRole, userRow.User_ID);
                    var refreshToken = JwtHelper.GenerateRefreshToken(userRow.User_ID);

                    //SetFormsAuthentication(userRow.Role_ID);
                    FormsAuthentication.SetAuthCookie(strRole, false);
                    // Store user information in the session
                    //Session["UserID"] = userRow.User_ID;
                    //Session["Fullname"] = fullname;
                    //Session["Role"] = strRole;

                    results.StatusCode = 200;
                    results.Message = "Login Successfully";
                    results.Data =  new { fullname = userRow.Fullname, access_token = token, refresh_token = refreshToken };
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

            return results;
        }

        public Task<bool> ResetPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserRegistration(object parameters)
        {
            throw new NotImplementedException();
        }





        //public void SetFormsAuthentication(int userID)
        //{
        //    // Set Forms Authentication ticket
        //    var authTicket = new FormsAuthenticationTicket(
        //        1,
        //        GlobalUtilities.UserRolesname(userID), // This becomes Identity.Name
        //        DateTime.Now,
        //        DateTime.Now.AddMinutes(30),
        //        false,
        //        userID.ToString() // user data - optional
        //    );

        //    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        //    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //    Response.Cookies.Add(authCookie);
        //}
    }
}