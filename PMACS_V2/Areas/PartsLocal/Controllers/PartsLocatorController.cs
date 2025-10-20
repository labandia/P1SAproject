using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Controllers;
using PMACS_V2.Utilities;
using ProgramPartListWeb.Helper;

namespace PMACS_V2.Areas.PartsLocal.Controllers
{
    public class PartsLocatorController : ExtendController
    {
        private readonly IProducts _prod;

        public PartsLocatorController(IProducts prod) => _prod = prod;

        [JwtAuthorize]
        public async Task<ActionResult> GetProductMasterlist()
        {
            var data = await _prod.GetRotorMasterlist() ?? new List<RotorProductModel>();

            if(data == null && data.Any()) return JsonNotFound("No Masterlist Data.");
          
            return JsonSuccess(data);
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetStorageData()
        {
            var data = await _prod.GetRotorStorage() ?? new List<RotorProductModel>();

            if (data == null && data.Any()) return JsonNotFound("No Storage Data.");

            return JsonSuccess(data);
        }


        [JwtAuthorize]
        public async Task<ActionResult> GetProductDetails(string partnum, int area)
        {
            var data = await _prod.GetRotorMasterlist() ?? new List<RotorProductModel>();

            var filteredData = data.SingleOrDefault(d => d.Partnumber == partnum && d.Area == area);

            if (filteredData == null)
                return JsonNotFound("No Rotor data found");

            return JsonSuccess(data);
        }



        // GET: PartsLocal/Masterlist
        public ActionResult TrackParts() { return View(); }
        // GET: PartsLocal/Masterlist
        public ActionResult Masterlist() { return View(); }
        // GET: PartsLocal/Masterlist
        public ActionResult StorageLocation() { return View(); }
        // GET: PartsLocal/SummaryShopOrderIn
        public ActionResult SummaryShopOrderIn() { return View(); }
        // GET: PartsLocal/SummaryShopOrderOut
        public ActionResult SummaryShopOrderOut() { return View(); }
        // GET: PartsLocal/ProductDetails/:StorageID
        public ActionResult ProductDetails(int StorageID)
        {
            ViewBag.Storage_ID = StorageID;
            return View();
        }
        // GET: PartsLocal/Index
        public ActionResult Index() { return View(); }
    }
}