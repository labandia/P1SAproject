using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Press
{
    public class PressAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Press";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Press_default",
                "Press/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}