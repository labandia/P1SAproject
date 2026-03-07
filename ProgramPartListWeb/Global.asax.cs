using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Repository;
using ProgramPartListWeb.Areas.Press.Interfaces;
using ProgramPartListWeb.Data;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Repository;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;
using NLog;
using System.Security.Principal;
using System.Web.Security;
using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Repository;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Repository;
using ProgramPartListWeb.Areas.Rotor.Data;
using ProgramPartListWeb.Areas.Rotor.Interface;
using Unity.Lifetime;
using System.Data.SqlClient;
using ProgramPartListWeb.Helper;

namespace ProgramPartListWeb
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


            PreloadRoutes();    
        }

        protected void Application_End()
        {
            SqlDependency.Stop(SqlDataAccess.BuildConnectionString());

            if (Response.StatusCode == 401)
            {
                Response.SuppressFormsAuthenticationRedirect = true;
            }
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

            // Repositories (Stateless → Singleton)
            container.RegisterType<IAuthRepository, AuthRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IUserRepository, UserRespository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmployee, EmployeeRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAluminumProducts, PressRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IInspector, InpectorRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHyrdoParts, HydroPartsRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRegistration, RegistrationRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPlanSchedule, PlanScheduleRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IChambers, ChamberRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPartsList, PartsMasterlistRepository>(new ContainerControlledLifetimeManager());
            container.RegisterType<IStocksparts, StockpartsRepository>(new ContainerControlledLifetimeManager());

            // Services (Usually stateless → Singleton)
            container.RegisterType<ICategory, CategoryServices>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRotorRegistration, RegistrationServices>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMaskMasterlist, MetalMaskServices>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMetalMast_Transaction, MetalMaskTransactionServices>(new ContainerControlledLifetimeManager());
            container.RegisterType<IStockAlertService, StockAlertService>(new ContainerControlledLifetimeManager());

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

        private void PreloadRoutes()
        {
            var hosts = new[]
            {
                "https://pmacsweb.sdp.com",
                "https://p1saportalweb.sdp.com",
                "http://localhost"
            };

            foreach (var host in hosts)
            {
                var request = new HttpRequest("", host, "");
                var response = new HttpResponse(null);
                var context = new HttpContext(request, response);
                var wrapper = new HttpContextWrapper(context);

                foreach (RouteBase route in RouteTable.Routes)
                {
                    route.GetRouteData(wrapper);
                }
            }
        }

    }
}
