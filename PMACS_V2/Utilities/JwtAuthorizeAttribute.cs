using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ProgramPartListWeb.Helper
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorizationHeader = httpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                return false;

            var token = authorizationHeader.Replace("Bearer ", "").Trim();

            if (string.IsNullOrEmpty(token))
                return false;

            var principal = JwtHelper.ValidateToken(token);

            if (principal == null)
                return false;

            httpContext.User = principal;
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = 401; // Unauthorized
            filterContext.Result = new JsonResult
            {
                Data = new { message = "Unauthorized" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}