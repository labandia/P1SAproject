using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMACS_V2.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Unauthorized()
        {
            return View();
        }


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