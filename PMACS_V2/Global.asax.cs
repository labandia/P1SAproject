using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Repository;
using PMACS_V2.Controllers;
using PMACS_V2.Interface;
using PMACS_V2.Repository;
using System.Web;
using System;
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
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Repository;
using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.MoldDie.Repository;

namespace PMACS_V2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            string homeMachineName = "DESKTOP-FC0UP1P";
            string homePath = @"C:\Users\Jaye Labandia\Desktop\Samplelogs";
            string officePath = @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\WebLogs";

            string selectedPath =
                string.Equals(Environment.MachineName, homeMachineName, StringComparison.OrdinalIgnoreCase)
                    ? homePath
                    : officePath;

            LogManager.Configuration.Variables["logDirectory"] = selectedPath;
            LogManager.ReconfigExistingLoggers(); // Apply change

            // DEPENDENCY INJECTION CONFIGURATION
            AreaRegistration.RegisterAllAreas();
            RegisterDependencyInjection();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

  
        protected void Application_EndRequest()
        {
            if (Response.StatusCode != 404)
                return;

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(Context));
            if (routeData == null)
            {
                Response.Clear();
                Server.TransferRequest("~/Error/NotFound");
            }
        }
        // ===== Clean Error Handling =====
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
        // ===== Safe Authentication Handling =====
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return;

            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
            if (ticket == null || ticket.Expired)
                return;

            var identity = new GenericIdentity(ticket.Name);
            var roles = string.IsNullOrEmpty(ticket.UserData)
                ? new string[] { }
                : new[] { ticket.UserData };

            HttpContext.Current.User = new GenericPrincipal(identity, roles);
            HttpContext.Current.Items["UserID"] = ticket.UserData;
        }
        private void RegisterDependencyInjection()
        {
            var container = new UnityContainer();

            // Register Repository Interface with Implementation
            container.RegisterType<IAuthRepository, AuthRepository>();
            container.RegisterType<IUserRepository, UserRespository>();
            container.RegisterType<IProducts, RotorProductRepository>();

            container.RegisterType<IShopOrderIn, RotorSummaryRepository>();
            container.RegisterType<IShopOrderOut, RotorSummaryRepositoryOut>();

            container.RegisterType<IPlanning, PlanningRepository>();
            container.RegisterType<IManpower, ManpowerRepository>();
            container.RegisterType<ICapacity, CapacityRepository>();
            container.RegisterType<IMachine, MachineRepository>();
            container.RegisterType<IDieMold, MoldDieRepository>();
            container.RegisterType<IMoldDieModel, MoldDieServices>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
