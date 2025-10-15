using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Hydroponics
{
    public class HydroponicsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Hydroponics";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Hydroponics_default",
                "Hydroponics/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}