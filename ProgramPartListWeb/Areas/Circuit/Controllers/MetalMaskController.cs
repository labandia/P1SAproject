using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Circuit.Controllers
{
    public class MetalMaskController : ExtendController
    {
        private readonly IMaskMasterlist _mas;
        private readonly IMetalMast_Transaction _trans;


        public MetalMaskController(IMaskMasterlist mas, IMetalMast_Transaction trans)
        {
            _mas = mas;
            _trans = trans;
        }


        [HttpGet]
        public async Task<ActionResult> SearchMetalMaskPartnum(string Partnumber)
        {
            var data = await _mas.SearchMetalMaskData(Partnumber);
            if (data == null && data.Any()) return JsonNotFound("No Masterlist Data.");
            return JsonSuccess(data);
        }




        // GET: Circuit/MetalMask
        public ActionResult Index()
        {
            return View();
        }
    }
}