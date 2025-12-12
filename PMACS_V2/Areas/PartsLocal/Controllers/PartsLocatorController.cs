using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Controllers;
using ProgramPartListWeb.Helper;

namespace PMACS_V2.Areas.PartsLocal.Controllers
{
    public class PartsLocatorController : ExtendController
    {
        private readonly IProducts _prod;
        private readonly IShopOrderIn _shopin;
        private readonly IShopOrderOut _shopout;    

        public PartsLocatorController(IProducts prod, IShopOrderIn shopin, IShopOrderOut shopout)
        {
            _prod = prod;
            _shopin = shopin;
            _shopout = shopout; 
        }

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
        public async Task<ActionResult> GetProductDetails(int RecordID)
        {
            var data = await _prod.GetRotorStorageByID(RecordID) ?? new RotorProductModel { };
            if (data == null)  return JsonNotFound("No Rotor data found");
            return JsonSuccess(data);
        }

        [HttpPost]
        public async Task<ActionResult> AddShopOrderIn(ShopOrderInModel shop)
        {
            bool result = await _shopin.AddTransactionIN(shop);
            if (!result) return JsonValidationError();
            return JsonCreated(shop, "Update successfully");
        }

        [HttpPost]
        public async Task<ActionResult> AddShopOrderOut(ShopOrderOutModel shop)
        {
            bool result = await _shopout.AddTransactionOut(shop);
            if (!result) return JsonValidationError();
            return JsonCreated(shop, "Update successfully");
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetTransactionSummaryIn()
        {
            var data = await _shopin.GetShopOderInlist() ?? new List<ShopOrderInModel>();
            if (data == null && data.Any()) return JsonNotFound("No Masterlist Data.");
            return JsonSuccess(data);
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetTransactionSummaryOut()
        {
            var data = await _shopout.GetShopOderOutlist() ?? new List<ShopOrderOutModel>();
            if (data == null && data.Any()) return JsonNotFound("No Masterlist Data.");
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