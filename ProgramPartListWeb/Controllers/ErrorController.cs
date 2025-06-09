using System.Web.Mvc;

namespace ProgramPartListWeb.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Unauthorized() => View();
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }
        public ActionResult ServerError()
        {
            Response.StatusCode = 500;
            return View("ServerError");
        }
        public ActionResult General()
        {
            Response.StatusCode = 400;
            return View("General");
        }
    }
}