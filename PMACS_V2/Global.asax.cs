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
using PMACS_V2.Helper;
using System.Data.SqlClient;
using Unity.Lifetime;

namespace PMACS_V2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureLogging();

            try
            {
                SqlDataAccess.StartSqlDependency();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            // DEPENDENCY INJECTION CONFIGURATION
            AreaRegistration.RegisterAllAreas();
            RegisterDependencyInjection();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_End()
        {
            SqlDependency.Stop(SqlDataAccess.BuildConnectionString());
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
            Exception ex = Server.GetLastError();
            Server.ClearError();

            if (ex is HttpException httpEx)
            {
                if (httpEx.GetHttpCode() == 404)
                {
                    Response.Redirect("~/Error/NotFound");
                    return;
                }
            }

            Response.Redirect("~/Error/ServerError");
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
            container.RegisterType<IAuthRepository, AuthRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserRepository, UserRespository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProducts, RotorProductRepository>(new ContainerControlledLifetimeManager());

            container.RegisterType<IShopOrderIn, RotorSummaryRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IShopOrderOut, RotorSummaryRepositoryOut>(new ContainerControlledLifetimeManager());

            container.RegisterType<IPlanning, PlanningRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IManpower, ManpowerRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICapacity, CapacityRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMachine, MachineRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDieMold, MoldDieRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMoldDieModel, MoldDieServices>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private void ConfigureLogging()
        {
            string homeMachineName = "DESKTOP-FC0UP1P";
            string homePath = @"C:\Users\Jaye Labandia\Desktop\Samplelogs";
            string officePath = @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\WebLogs";

            string selectedPath =
                string.Equals(Environment.MachineName, homeMachineName, StringComparison.OrdinalIgnoreCase)
                    ? homePath
                    : officePath;

            // Ensure directory exists
            if (!System.IO.Directory.Exists(selectedPath))
            {
                System.IO.Directory.CreateDirectory(selectedPath);
            }

            // Apply to NLog variable
            var config = LogManager.Configuration;

            if (config != null)
            {
                config.Variables["logDirectory"] = selectedPath;
                LogManager.ReconfigExistingLoggers();
            }
        }

    }
}
