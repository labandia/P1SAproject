using ProgramPartListWeb.Helper;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class GlobalErrorException : FilterAttribute, IExceptionFilter
{
    private readonly string logPath = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.txt");

    public void OnException(ExceptionContext filterContext)
    {
        if (filterContext.ExceptionHandled)
            return;

        Exception ex = filterContext.Exception;

        // Log the exception
        //LogException(ex);

        // Redirect to a friendly error page
        filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary
            {
                { "controller", "Error" },
                { "action", "ServerError" }
            });

        filterContext.ExceptionHandled = true;
    }

    private void LogException(Exception ex)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine("==== Exception ====");
                writer.WriteLine("Time: " + DateTime.Now);
                writer.WriteLine("Message: " + ex.Message);
                writer.WriteLine("StackTrace: " + ex.StackTrace);
                writer.WriteLine();
            }
        }
        catch
        {
        }
    }
}