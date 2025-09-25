
using Microsoft.AspNet.SignalR;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Hydroponics.Controllers
{
    public class HydroController : ExtendController
    {
        private readonly IHyrdoParts _hydro;
        private readonly IPartsList _partsList;
        private readonly IChambers _chambers;
        private readonly IStocksparts _stock;

        public HydroController(IHyrdoParts hydro, IPartsList partsList, IChambers chambers, IStocksparts stock)
        {
            _hydro = hydro;
            _partsList = partsList;
            _chambers = chambers;
            _stock = stock;
        }

        [HttpGet]
        public async Task<JsonResult> GetStockAlerts()
        {
            var data = await _stock.GetStocksAlertList() ?? new List<StockAlertModel>();

            return Json(new
            {
                Success = true,
                Alerts = data,
                LastCheck = DateTime.Now
            }, JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public async  Task<ActionResult>EditPartslist(MasterlistPartsModel model, HttpPostedFileBase partsimage)
        {
            string fullnamefile = "";
          
            if (partsimage != null && partsimage.ContentLength > 0)
            {
                string exportFolder = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\HydroParts\";

                if (!Directory.Exists(exportFolder))
                    Directory.CreateDirectory(exportFolder);

                // Get the file name and extension and store to fullpath 
                // Create unique file name
                string fileName = Path.GetFileNameWithoutExtension(partsimage.FileName);
                string extension = Path.GetExtension(partsimage.FileName);


                // Create unique filename (avoid overwriting existing files)
                fullnamefile = fileName + "_" + Guid.NewGuid().ToString("N").Substring(0, 6) + extension;
                Debug.WriteLine(fullnamefile);
                string fullPath = Path.Combine(exportFolder, fullnamefile);


                Debug.WriteLine(fullPath);
                // Save the uploaded file to disk
                partsimage.SaveAs(fullPath);

                model.ImageParts = fullnamefile;

            }

            // Overwrite the image path if there is a new image upload to MastelistPartModel

            bool result = await  _partsList.EditMasterlistParts(model);
            if (!result) return JsonPostError("Insert failed.", 500);

            CacheHelper.Remove("MaterialPartlist");

            return JsonCreated(model, "Edit Masterlist Successfully");

        }



        //-----------------------------------------------------------------------------------------
        //----------------------------  CHAMBER LIST PAGE -----------------------------------------
        //-----------------------------------------------------------------------------------------
        [JwtAuthorize]
        public async Task<ActionResult> GetTotalPriceChamber(int chamber)
        {
            var data = await _chambers.GetTotalPriceData(chamber);
            if (data == null) return JsonNotFound("No Chamber list Data.");

            return JsonSuccess(data, "Load Total Price");
        }
        [JwtAuthorize]
        public async Task<ActionResult> GetAllChamberList(int chamber)
        {
            if (chamber <= 0)
            {
                return JsonMultipleData(null, "Invalid chamber .");
            }

            // Run tasks in parallel for better performance
            var tableListTask = _chambers.GetChambersData(chamber);
            var totalPriceTask = _chambers.GetTotalPriceData(chamber);
            var produceTask = _chambers.GetTotalChamberProduce(chamber);

            await Task.WhenAll(tableListTask, totalPriceTask, produceTask);

            var tableList = tableListTask.Result ?? new List<ChamberModel>();
            var totalPrice = totalPriceTask.Result;
            var produce = produceTask.Result;

            // All in One Display
            var multiData = new Dictionary<string, object>
            {
                { "GetList", tableList },
                { "TotalPrice", totalPrice },
                { "Produce", produce }
            };

            return JsonMultipleData(multiData);
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetChamberDropDownList()
        {
            var data = await _chambers.GetChamberTypes() ?? new List<ChamberTypeList>();
            if (data == null || !data.Any()) return JsonNotFound("No Chamber list Data.");

            return JsonSuccess(data, "Load Chamber type List");
        }

        [HttpPost]
        public async Task<ActionResult> ChangeCostChamber(int ChamberPartID, string UnitCost_PHP)
        {
            try
            {
                bool result = await _chambers.UpdateUnitCostChamber(ChamberPartID, UnitCost_PHP);
                if (!result) return JsonPostError("Update failed.", 500);
                return JsonCreated(result, "Update Unit Cost Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
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

        [HttpPost]
        public async Task<ActionResult> UpdateWarningStock(int StockID, double WarningLevel)
        {
            try
            {
                bool result = await _hydro.UpdateWarning(StockID, WarningLevel);
                if (!result) return JsonPostError("Update failed.", 500);
                return JsonCreated(result, "Update Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }





        //-----------------------------------------------------------------------------------------
        //---------------------------- REQUEST CHAMBER PAGE ---------------------------------------
        //-----------------------------------------------------------------------------------------
        [JwtAuthorize]
        public async Task<ActionResult> GetAllRequestList()
        {
            var data = await _chambers.GetRequestList() ?? new List<RequestChambersModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Request list Data.");

            return JsonSuccess(data, "Load Request List");
        }

    

        [JwtAuthorize]
        public async Task<ActionResult> GetMainRequestList(string orderID)
        {
            var data = await _chambers.GetRequestList() ?? new List<RequestChambersModel>();

            var filterdata = data.SingleOrDefault(res => res.OrderID == orderID);
            if (filterdata == null) return JsonNotFound("No Request list Data.");

            return JsonSuccess(filterdata, "Load Request List");
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetRequestDetails(string orderID)
        {

            var data = await _chambers.GetRequestDetailList(orderID) ?? new List<RequestChambersDetailsModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Request Details list Data.");

            return JsonSuccess(data, "Load Request Details List");
        }


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

            var data = await _hydro.GetChamberTypePartsList(chamberID) ?? new List<ChamberTypePartsModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Chamber list Data.");

            return JsonSuccess(data, "Load Chamber type List");
        }


        [HttpPost]
        public async Task<ActionResult> ChangeStatusRequest(int OrderID, string RequestStatus)
        {
            try
            {
                bool result = await _chambers.UpdateRequestStatus(OrderID, RequestStatus);
                if (!result) return JsonPostError("Update failed.", 500);


                return JsonCreated(result, "Update Status Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }


        [HttpPost]
        public async Task<ActionResult> NewAddRequestData(RequestItem item)
        {
            try
            {
                bool result = await _chambers.AddRequestChamber(item);
                if (!result) return JsonPostError("Insert failed.", 500);


                return JsonCreated(item, "Update Stocks Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }

        }

        [HttpPost]
        public async Task<ActionResult> UpdatesRequestMaterials(int OrderDetailID, double NewUsedQuan)
        {
            try
            {
                //bool result = await _chambers.UpdatesRequestMaterials(OrderDetailID, NewUsedQuan);
                //if (!result) return JsonPostError("Update failed.", 500);
                return JsonCreated(true, "Update Stocks Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }

        }

        [HttpPost]
        public async Task<ActionResult> UpdatesRequestMaterialsV2([System.Web.Http.FromBody]  List<AllocationRequest> allo)
        {
            try
            {
                foreach (var item in allo)
                {
                    Debug.WriteLine($"Order ID : {item.OrderID} - Part ID : {item.PartID} - Allocated :  {item.allocated}");
                    await _chambers.UpdatesRequestMaterials(item.OrderID, item.PartID, item.allocated);
                }

                return Json(new { Success = true, Message = "Allocations saved successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }

        }



        [HttpPost]
        public async Task<ActionResult> SaveStocks()
        {
            try
            {
                var json = Request.Form["items"];

                var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockItem>>(json);

                foreach (var item in items)
                {
                    await _stock.AddStocks(item.PartID, item.Quantity);
                }

                return JsonCreated(true, "Add Stocks Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }

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

        public ActionResult DisplaytheImage(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return HttpNotFound();

            string folderPath = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\HydroParts\";
            string fullPath = Path.Combine(folderPath, filename);

            if (!System.IO.File.Exists(fullPath))
                return File("~/Content/Images/bussiness-man.png", "image/png");

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, "image/png"); // Change to "image/jpeg" if needed
        }


        // GET: Hydroponics/Index
        public ActionResult Index() => View();
        // GET: Hydroponics/Inventorylist
        public ActionResult Inventorylist() => View();
        // GET: Hydroponics/Inventorylist
        public ActionResult AddStocks() => View();
        // GET: Hydroponics/Orderpage
        public ActionResult Orderpage() => View();
        // GET: Hydroponics/OrderpageDetails
        public ActionResult OrderpageDetails(string orderID)
        {
            if(orderID == "")
            {
                return RedirectToAction("Orderpage", "Hydro", new { area = "Hydroponics" });
            }
            return View();
        }
        // GET: Hydroponics/Chambers
        public ActionResult Chambers()
        {
            return View();
        }

        // GET: Hydroponics/PartList
        public ActionResult PartList() => View();
        // GET: Hydroponics/UserManage
        public ActionResult TransactionHistory() => View();
        // GET: Hydroponics/UserManage
        public ActionResult UserManage() => View();
        // GET: Hydroponics/PartList
        public ActionResult SampleLayout() => View();
    }
}