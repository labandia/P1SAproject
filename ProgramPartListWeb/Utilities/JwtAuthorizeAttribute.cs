using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ProgramPartListWeb.Helper
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _roles;

        public JwtAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                var authHeader = httpContext.Request.Headers["Authorization"];
                if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                    return false;

                var token = authHeader.Substring("Bearer ".Length).Trim();
                var principal = JwtHelper.ValidateToken(token);

                httpContext.User = principal;
                Thread.CurrentPrincipal = principal;

                if (_roles == null || _roles.Length == 0)
                    return true;

                var identity = principal.Identity as ClaimsIdentity;
                var userRoles = identity?.FindAll(ClaimTypes.Role).Select(r => r.Value);

                return userRoles != null && _roles.Any(role => userRoles.Contains(role));
            }
            catch
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            // Detect if it's a Fetch/XHR request by checking the headers manually
            bool isApiRequest = request.Headers["Authorization"] != null
                                || request.AcceptTypes?.Any(t => t.Contains("application/json")) == true;

            if (isApiRequest)
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        message = "Unauthorized: Access is denied due to invalid token or insufficient permissions."
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                // Important: Set the actual HTTP status code to 401 for fetch to recognize it
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
            }
            else
            {
                // Fallback for normal browser requests
                filterContext.Result = new RedirectResult("~/Error/Unauthorized");
            }
        }
    }
}