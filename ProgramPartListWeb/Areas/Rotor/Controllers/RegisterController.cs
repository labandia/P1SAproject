using DocumentFormat.OpenXml.Office2019.Drawing.Ink;
using ProgramPartListWeb.Areas.Rotor.Interface;
using ProgramPartListWeb.Areas.Rotor.Model;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<ActionResult> GetRegistrationList()
        {
            var result = await _reg.GetRegistrationYear();
            if (result == null || !result.Any()) return JsonNotFound("No Tranasctioon Data.");
            return JsonCreated(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetRegistrationInformation(
           string search,
           int monthfilter,
           int yearint, 
           int catID,
           int depID)
        {

            Debug.WriteLine($@"Month2: {monthfilter} - Year : {yearint} - category : {catID} - Depart : {depID}");
            var result = await _reg.GetRegistrationsList(search, monthfilter, yearint, catID, depID, 0, 0);

            if (result == null || !result.Items.Any()) return JsonNotFound("No Tranasctioon Data.");
            return JsonSuccess(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewRegistration(RotorRegistrationModel model) { 
            var result = await _reg.AddRegistration(model);

            if (!result) return JsonPostError("Error Post Data");

            return JsonCreated(result, "Add new Registraiton No successfully");
        }

        // GET: Rotor/Register
        public ActionResult Index()
        {
            return View();
        }
    }
}