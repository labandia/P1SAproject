using System.Web.Mvc;

namespace PMACS_V2.Areas.MoldDie
{
    public class MoldDieAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MoldDie";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MoldDie_default",
                "MoldDie/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}