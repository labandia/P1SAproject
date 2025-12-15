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


        // ===========================================================
        // =============== MASTERLIST PAGES FUNCTION  ================
        // ===========================================================
        [JwtAuthorize]
        public async Task<ActionResult> GetProductMasterlistPagination(
            string search = "",
            int page = 1,
            int pageSize = 10)
        {
            var data = await _prod.GetRotorMasterlistPage(search, page, pageSize);
            if (data == null && data.Items.Any()) return JsonNotFound("No Masterlist Data.");
            return JsonSuccess(data);
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetProductsLocationPallete(string partnum = "")
        {
            var data = await _prod.GetRotorStorage() ?? new List<RotorProductModel>();

            var filterdata = data.Where(res => res.Partnumber == partnum);

            if (filterdata == null && filterdata.Any()) return JsonNotFound("No Masterlist Data.");
            return JsonSuccess(filterdata);
        }

        [HttpPost]
        public async Task<ActionResult> AddMasterlist(RotorProductModel shop)
        {
            //bool result = await _prod.AddRotorMasterlist(shop);
            //if (!result) return JsonValidationError();
            return JsonCreated(shop, "Insert successfully");
        }

        [HttpPost]
        public async Task<ActionResult> EditMasterlist(RotorProductModel shop)
        {
            bool result = await _prod.UpdateRotorMasterlist(shop);
            if (!result) return JsonValidationError();
            return JsonCreated(shop, "Update successfully");
        }

        // ===========================================================
        // ================== TRACK LOCATION  ========================
        // ===========================================================

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
      

        // ===========================================================

        // ===========================================================
        // ================== SHOP ORDER SUMMARY  ====================
        // ===========================================================
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

        // ===========================================================



        // ===========================================================
        // ================== DISPLAY PAGES ==========================
        // ===========================================================

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