using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Production1
{
    public class Production1AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Production1";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Production1_default",
                "Production1/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}