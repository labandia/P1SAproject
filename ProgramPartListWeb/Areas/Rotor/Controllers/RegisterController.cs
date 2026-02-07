using DocumentFormat.OpenXml.Office2019.Drawing.Ink;
using ProgramPartListWeb.Areas.Rotor.Interface;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Rotor.Controllers
{
    public class RegisterController : ExtendController
    {
        private readonly IRotorRegistration _reg;

        public RegisterController(IRotorRegistration reg) => _reg = reg;

        [HttpGet]
        public async Task<ActionResult> GetRegistrationInformation(
           string search,
           int monthfilter,
           int catID,
           int depID)
        {

            var result = await _reg.GetRegistrationsList(search, monthfilter, catID, depID, 0, 0);

            if (result == null || !result.Items.Any()) return JsonNotFound("No Tranasctioon Data.");
            return JsonSuccess(result);
        }



        // GET: Rotor/Register
        public ActionResult Index()
        {
            return View();
        }
    }
}