using System.Web.Mvc;

namespace PMACS_V2.Areas.P1SA
{
    public class P1SAAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "P1SA";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "P1SA_default",
                "P1SA/{controller}/{action}/{id}",
               new { controller = "PMACS", action = "Mainpage", id = UrlParameter.Optional }
            );
        }
    }
}