using System.Web.Mvc;

namespace PMACS_V2.Areas.PartsLocal
{
    public class PartsLocalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PartsLocal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PartsLocal_default",
                "PartsLocal/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}