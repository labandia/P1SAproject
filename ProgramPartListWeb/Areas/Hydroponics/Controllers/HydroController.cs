
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Hydroponics.Controllers
{
    public class HydroController : ExtendController
    {
        private readonly IHyrdoParts _hydro;
        public HydroController(IHyrdoParts hydro) => _hydro = hydro;


        //-----------------------------------------------------------------------------------------
        //---------------------------- HYDRO INVENTORY PAGE ---------------------------------------
        //-----------------------------------------------------------------------------------------
        [JwtAuthorize]
        public async Task<ActionResult> GetHydroInventory()
        {
            var data = await _hydro.GetInventoryList() ?? new List<HydropPartsModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Hydro parlist Data.");

            return JsonSuccess(data, "Load Inventory List");
        }


        [JwtAuthorize]
        public async Task<ActionResult> GetCategorylist()
        {
            var data = await _hydro.GetInventoryList() ?? new List<HydropPartsModel>();

            var filterdata = data.GroupBy(x => new { x.CategoryID, x.CategoryName })
                .Select(g => new
                {
                    CategoryID = g.Key.CategoryID,
                    CategoryName = g.Key.CategoryName
                }).ToList();

            if (filterdata == null || !filterdata.Any()) return JsonNotFound("No Category list Data.");

            return JsonSuccess(filterdata, "No Category  List");
        }


        [HttpPost]
        public async Task<ActionResult> UpdateStocksQuantity()
        {
            try
            {
                int PartID = Convert.ToInt32(Request.Form["AddPartID"]);
                int CurrentQty = Convert.ToInt32(Request.Form["CurrentQty"]);

                bool result = await _hydro.UpdateStocks(PartID, CurrentQty);

                if (!result) return JsonPostError("Insert failed.", 500);

                //CacheHelper.Remove("Inspectors");
                return JsonCreated(result, "Update Stocks Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }

        }


        // GET: Hydroponics/Index
        public ActionResult Index() => View();
        // GET: Hydroponics/Inventorylist
        public ActionResult Inventorylist() => View();
        // GET: Hydroponics/Orderpage
        public ActionResult Orderpage() => View();
    }
}