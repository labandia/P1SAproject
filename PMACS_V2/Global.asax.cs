using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Repository;
using PMACS_V2.Controllers;
using PMACS_V2.Interface;
using PMACS_V2.Repository;
using System.Web;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;
using NLog;
using System.Security.Principal;
using System.Web.Security;
using PMACS_V2.Areas.Planning.Interface;
using PMACS_V2.Areas.Planning.Repository;
using System.IO.Compression;

namespace PMACS_V2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string homeMachineName = "DESKTOP-FC0UP1P";
            string currentMachine = Environment.MachineName.ToUpperInvariant();

            string homePath = @"C:\Users\Jaye Labandia\Desktop\Samplelogs";
            string officePath = @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\WebLogs";

            string selectedPath = currentMachine == homeMachineName ? homePath : officePath;

            LogManager.Configuration.Variables["logDirectory"] = selectedPath;
            LogManager.ReconfigExistingLoggers(); // Apply change

            // DEPENDENCY INJECTION CONFIGURATION
            AreaRegistration.RegisterAllAreas();
            RegisterDependencyInjection();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");

            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.StatusCode = 200;
                HttpContext.Current.Response.End();
            }

            HttpResponse response = HttpContext.Current.Response;

            string acceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];

            if (!string.IsNullOrEmpty(acceptEncoding))
            {
                acceptEncoding = acceptEncoding.ToLower();

                if (acceptEncoding.Contains("gzip"))
                {
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                    response.AppendHeader("Content-Encoding", "gzip");
                }
                else if (acceptEncoding.Contains("deflate"))
                {
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                    response.AppendHeader("Content-Encoding", "deflate");
                }
            }

            // Add Vary header
            response.AppendHeader("Vary", "Content-Encoding");
        }

        protected void Application_EndRequest()
        {
            var context = new HttpContextWrapper(Context);
            var routeData = RouteTable.Routes.GetRouteData(context);

            if (Context.Response.StatusCode == 404 && routeData == null)
            {
                Response.Clear();
                Server.TransferRequest("~/Error/NotFound");
            }
        }
        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            HttpContext httpContext = HttpContext.Current;

            if (httpContext != null)
            {
                var routeData = new RouteData();
                routeData.Values["controller"] = "Error";

                if (exception is HttpException httpException)
                {
                    switch (httpException.GetHttpCode())
                    {
                        case 404:
                            routeData.Values["action"] = "NotFound";
                            break;
                        case 500:
                            routeData.Values["action"] = "ServerError";
                            break;
                        default:
                            routeData.Values["action"] = "General";
                            break;
                    }
                }
                else
                {
                    routeData.Values["action"] = "General";
                }

                routeData.Values["exception"] = exception;

                Server.ClearError();

                IController errorController = new ErrorController();
                errorController.Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
        }
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                if (ticket != null && !ticket.Expired)
                {
                    var identity = new GenericIdentity(ticket.Name); // This is User_ID
                    var roles = new[] { ticket.UserData };           // Role name, optional
                    HttpContext.Current.User = new GenericPrincipal(identity, roles);
                }

                string userId = ticket.UserData;
                HttpContext.Current.Items["UserID"] = userId;
            }
        }
        private void RegisterDependencyInjection()
        {
            var container = new UnityContainer();

            // Register Repository Interface with Implementation
            container.RegisterType<IUserRepository, UserRespository>();
            container.RegisterType<IPlanning, PlanningRepository>();
            container.RegisterType<IManpower, ManpowerRepository>();
            container.RegisterType<ICapacity, CapacityRepository>();
            container.RegisterType<IMachine, MachineRepository>();
            container.RegisterType<IDieMold, MoldDieRepository>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
