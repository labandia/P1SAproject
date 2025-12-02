using ProgramPartListWeb.Utilities.Security;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ProgramPartListWeb.Helper
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _requiredRole;

        public JwtAuthorizeAttribute(string role = null)
        {
            _requiredRole = role;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authHeader = httpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                return false;

            var token = authHeader.Substring("Bearer ".Length);

            try
            {
                // Validate JWT
                var principal = JWTAuthentication.ValidateToken(token);
                if (principal == null)
                    return false;

                httpContext.User = principal; // Set the current principal
                System.Threading.Thread.CurrentPrincipal = principal;

                // No role required, token is enough
                if (string.IsNullOrEmpty(_requiredRole))
                    return true;

                // Validate role
                return principal.IsInRole(_requiredRole);
            }
            catch
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            bool isApiRequest = request.Headers["Authorization"] != null
                                || request.AcceptTypes?.Any(t => t.Contains("application/json")) == true;


            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;


            if (isApiRequest)
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new { success = false, message = "Unauthorized" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            }
            else
            {
                // Fallback for browser requests
                filterContext.Result = new RedirectResult("~/Error/Unauthorized");
            }
        }

    }
}