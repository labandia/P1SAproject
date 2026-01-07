using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PMACS_V2.Areas.PartsLocal.Interface;
using PMACS_V2.Areas.PartsLocal.Model;
using PMACS_V2.Controllers;
using PMACS_V2.Interface;
using ProgramPartListWeb.Helper;

namespace PMACS_V2.Areas.PartsLocal.Controllers
{
    public class PartsLocatorController : ExtendController
    {
        private readonly IProducts _prod;
        private readonly IShopOrderIn _shopin;
        private readonly IShopOrderOut _shopout;
        private readonly IUserRepository _user;

        public PartsLocatorController(IProducts prod, IShopOrderIn shopin, IShopOrderOut shopout, IUserRepository user)
        {
            _prod = prod;
            _shopin = shopin;
            _shopout = shopout;
            _user = user;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployeeInfo(string emp)
        {
            var data = await _user.GetEmployees(emp) ?? new List<Models.Employee>();
            if (data == null && data.Any()) return JsonNotFound("No Employees Data.");
            return JsonSuccess(data);
        }

        // ===========================================================
        // =============== MASTERLIST PAGES FUNCTION  ================
        // ===========================================================
        [JwtAuthorize]
        public async Task<ActionResult> GetProductMasterlistPagination(
            string search = "",
            int page = 1,
            int pageSize = 100)
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
            bool result = await _prod.AddRotorMasterlist(shop);
            if (!result) return JsonValidationError();
            return JsonCreated(shop, "Insert successfully");
        }

        [HttpPost]
        public async Task<ActionResult> EditMasterlist(RotorProductModel shop)
        {
            bool result = await _prod.UpdateRotorMasterlist(shop);
            if (!result) return JsonValidationError();
            return JsonCreated(shop, "Update successfully");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMasterListpartnum(string partnum)
        {
            bool result = await _prod.DeleteMasterlist(partnum);
            if (!result) return JsonValidationError();
            return JsonCreated(result, "Delete Masterlist successfully");
        }


        [HttpPost]
        public async Task<ActionResult> AddMasterlocation(int area, string partnum, int Quantity)
        {
            bool result = await _prod.AddNewLocation(area, partnum, Quantity);
            if (!result) return JsonValidationError();
            return JsonCreated(result, "Add new Palete location successfully");
        }

        [HttpPost]
        public async Task<ActionResult> EditMasterlocation(int RecordID, int newarea, int Quantity)
        {
            bool result = await _prod.ChangeLocation(RecordID, newarea, Quantity);
            if (!result) return JsonValidationError();
           
            return JsonCreated(result, "Change new palete location successfully");
        }


        [HttpPost]
        public async Task<ActionResult> DeleteMasterlocation(int recordID)
        {
            bool result = await _prod.RemoveLocation(recordID);
            if (!result) return JsonValidationError();
            return JsonCreated(result, "Delete Masterlist successfully");
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

        [HttpPost]
        public async Task<ActionResult> EditShopOrderOut(ShopOrderOutModel shop)
        {
            bool result = await _shopout.EditTransactionOut(shop);
            if (!result) return JsonValidationError();
            return JsonCreated(shop, "Update successfully");
        }

        [HttpPost]
        public async Task<ActionResult> EditShopOrderIn(ShopOrderInModel shop)
        {
            bool result = await _shopin.EditTransaction(shop);
            if (!result) return JsonValidationError();
            return JsonCreated(shop, "Update successfully");
        }



        [JwtAuthorize]
        public async Task<ActionResult> GetTransactionSummaryIn(
            DateTime? startDate,
            DateTime? endDate,
            string search,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var from = startDate ?? DateTime.Today;
            var to = endDate ?? DateTime.Today;

            var data = await _shopin.GetShopOderInlist(
                    from.Date,
                    to.Date,
                    search,
                    pageNumber,
                    pageSize) ?? new List<ShopOrderInModel>();
            if (data == null && data.Any()) return JsonNotFound("No Shop Summary In Data.");
            return JsonSuccess(data);
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetTransactionSummaryOut(
            DateTime? startDate,
            DateTime? endDate,
            string search,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var from = startDate ?? DateTime.Today;
            var to = endDate ?? DateTime.Today;

            var data = await _shopout.GetShopOderOutlist(
                    from.Date,
                    to.Date,
                    search,
                    pageNumber,
                    pageSize) ?? new List<ShopOrderOutModel>();
            if (!data.Any())
                return JsonNotFound("No Shop Summary Out Data.");
            // Fallback defaults

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