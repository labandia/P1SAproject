using System.Web.Mvc;

namespace PMACS_V2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Mainpage", "PMACS", new { area = "P1SA" });
        }
    }
}