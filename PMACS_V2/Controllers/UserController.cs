using System.Diagnostics;
using System.Security.Claims;
using System.Web.Mvc;
using PMACS_V2.Interface;
using PMACS_V2.Utilities.Security;

namespace PMACS_V2.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _user;

        public UserController(IUserRepository user) => _user = user;


        public class RefreshTokenModel
        {
            public string RefreshToken { get; set; }
        }


        [HttpPost]
        public JsonResult RefreshToken(RefreshTokenModel request)
        {
            Debug.WriteLine("Request : " + request.RefreshToken);

            var principal = JWTAuthentication.ValidateRefreshToken(request.RefreshToken);

            if (principal == null)
                return Json(new { success = false, message = "Invalid refresh token" });


            string userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var fullname = principal.FindFirst("Fullname")?.Value;
            string role = principal.FindFirst(ClaimTypes.Role)?.Value;

            Debug.WriteLine("User ID : " + userId);
            Debug.WriteLine("fullname : " + fullname);
            Debug.WriteLine("role : " + role);
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


    }
}