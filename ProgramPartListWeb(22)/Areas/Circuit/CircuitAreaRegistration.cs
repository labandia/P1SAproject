using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Circuit
{
    public class CircuitAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Circuit";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Circuit_default",
                "Circuit/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}