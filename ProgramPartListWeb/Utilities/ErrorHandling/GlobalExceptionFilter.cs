
using System.Collections.Generic;
using System;
using System.Web.Mvc;
using ProgramPartListWeb.Models;

namespace ProgramPartListWeb.Utilities
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled) return;

            var ex = filterContext.Exception;
            var request = filterContext.HttpContext.Request;
            int status = 500;
            string title = "An unexpected error occurred.";

            if (ex is ArgumentException) { status = 400; title = "Bad Request"; }
            else if (ex is KeyNotFoundException) { status = 404; title = "Not Found"; }

            var problem = new ProblemDetails
            {
                Title = title,
                Status = status,
                Detail = ex.Message,
                Instance = request.Url?.AbsoluteUri
            };

            filterContext.Result = new ProblemDetailsActionResult(problem);
            filterContext.ExceptionHandled = true;
        }
    }
}