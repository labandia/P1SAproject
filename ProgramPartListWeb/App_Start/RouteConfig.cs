using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProgramPartListWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Press_Partslocator",
               url: "Press_Partslocator/{controller}/{action}/{id}",
               defaults: new { controller = "PartslocatorAluminum", action = "PressPartslocator", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "P1SAportalweb", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
