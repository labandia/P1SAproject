using System.Collections.Generic;
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
        public PartsLocatorController(IProducts prod) {
            _prod = prod;
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetProductMasterlist()
        {
            var data = await _prod.GetRotorMasterlist() ?? new List<RotorProductModel>();
            if (data == null)
                return JsonNotFound("No Rotor data found");

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
        [CompressResponse]
        public ActionResult Masterlist() { return View(); }
        // GET: PartsLocal/Masterlist
        [CompressResponse]
        public ActionResult StorageLocation() { return View(); }
        // GET: PartsLocal/SummaryShopOrderIn
        [CompressResponse]
        public ActionResult SummaryShopOrderIn() { return View(); }
        // GET: PartsLocal/SummaryShopOrderOut
        [CompressResponse]
        public ActionResult SummaryShopOrderOut() { return View(); }
        // GET: PartsLocal/ProductDetails/:StorageID
        [CompressResponse]
        public ActionResult ProductDetails(int StorageID)
        {
            ViewBag.Storage_ID = StorageID;
            return View();
        }
        // GET: PartsLocal/Index
        public ActionResult Index() { return View(); }
    }
}