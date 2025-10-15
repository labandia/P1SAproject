using ProgramPartListWeb.Data;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Utilities
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _allowedRoles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userIdStr = httpContext.User.Identity.Name;
            if (string.IsNullOrEmpty(userIdStr))
                return false;

            return _allowedRoles.Any(role => role.Equals(userIdStr, StringComparison.OrdinalIgnoreCase));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Error/Unauthorized");
        }
    }



}