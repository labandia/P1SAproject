using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Controllers
{
    public class StockController : Controller
    {
        public ActionResult Chat()
        {
            return View();
        }

    }
}