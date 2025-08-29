
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Hydroponics.Controllers
{
    public class HydroController : ExtendController
    {
        private readonly IHyrdoParts _hydro;
        private readonly IPartsList _partsList;
        private readonly IChambers _chambers;

        public HydroController(IHyrdoParts hydro, IPartsList partsList, IChambers chambers)
        {
            _hydro = hydro;
            _partsList = partsList;
            _chambers = chambers;
        }
        //-----------------------------------------------------------------------------------------
        //---------------------------- COMMON CONTROLLERS  ----------------------------------------
        //-----------------------------------------------------------------------------------------
        [JwtAuthorize]
        public async Task<ActionResult> GetCategorylist()
        {
            var data = await _partsList.GetPartsMasterlist() ?? new List<MasterlistPartsModel>();

            var filterdata = data.GroupBy(x => new { x.CategoryID, x.CategoryName })
                .Select(g => new
                {
                    CategoryID = g.Key.CategoryID,
                    CategoryName = g.Key.CategoryName
                }).ToList();

            if (filterdata == null || !filterdata.Any()) return JsonNotFound("No Category list Data.");

            return JsonSuccess(filterdata, "No Category  List");
        }


        //-----------------------------------------------------------------------------------------
        //---------------------------- MASTERLIST PART LIST PAGE ----------------------------------
        //-----------------------------------------------------------------------------------------
        [JwtAuthorize]
        public async Task<ActionResult> GetPartlistData()
        {
            var data = await _partsList.GetPartsMasterlist() ?? new List<MasterlistPartsModel>();
            if (data == null || !data.Any()) return JsonNotFound("No parlist Masterlist Data.");

            return JsonSuccess(data, "Load parlist Masterlist");
        }

        //-----------------------------------------------------------------------------------------
        //----------------------------  CHAMBER LIST PAGE -----------------------------------------
        //-----------------------------------------------------------------------------------------
        [JwtAuthorize]
        public async Task<ActionResult> GetAllChamberList(int chamber)
        {
            var data = await _chambers.GetChambersData(chamber) ?? new List<ChamberModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Chamber list Data.");

            return JsonSuccess(data, "Load Chamber type List");
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetChamberDropDownList()
        {
            var data = await _chambers.GetChamberTypes() ?? new List<ChamberTypeList>();
            if (data == null || !data.Any()) return JsonNotFound("No Chamber list Data.");

            return JsonSuccess(data, "Load Chamber type List");
        } 



            //-----------------------------------------------------------------------------------------
            //---------------------------- HYDRO INVENTORY PAGE ---------------------------------------
            //-----------------------------------------------------------------------------------------
            [JwtAuthorize]
        public async Task<ActionResult> GetHydroInventory()
        {
            var data = await _hydro.GetInventoryList() ?? new List<StockPartsModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Hydro parlist Data.");

            return JsonSuccess(data, "Load Inventory List");
        }
      






        //-----------------------------------------------------------------------------------------
        //---------------------------- REQUEST CHAMBER PAGE ---------------------------------------
        //-----------------------------------------------------------------------------------------
        [JwtAuthorize]
        public async Task<ActionResult> GetChamberList()
        {
            var data = await _hydro.GetChambersType() ?? new List<ChamberTypeList>();
            if (data == null || !data.Any()) return JsonNotFound("No Chamber list Data.");

            return JsonSuccess(data, "Load Chamber type List");
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetChamberTypeDataList(int chamberID)
        {
            Debug.WriteLine("Chamber ID : " + chamberID);

            var data = await _hydro.GetChamberTypePartsList(chamberID) ?? new List<ChamberTypePartsModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Chamber list Data.");

            return JsonSuccess(data, "Load Chamber type List");
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
        // GET: Hydroponics/Inventorylist
        public ActionResult AddStocks() => View();
        // GET: Hydroponics/Orderpage
        public ActionResult Orderpage() => View();
        // GET: Hydroponics/Chambers
        public ActionResult Chambers() => View();

        // GET: Hydroponics/PartList
        public ActionResult PartList() => View();
    }
}