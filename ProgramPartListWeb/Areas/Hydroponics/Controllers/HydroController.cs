
using Aspose.Cells.Drawing;
using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;
using ProgramPartListWeb.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        private readonly IUserRepository _user;

        public HydroController(IHyrdoParts hydro, IPartsList partsList, IChambers chambers, IStocksparts stock, IUserRepository user)
        {
            _hydro = hydro;
            _partsList = partsList;
            _chambers = chambers;
            _stock = stock;
            _user = user;
        }
        //-----------------------------------------------------------------------------------------
        //---------------------------- USERS DATA  ------------------------------------------------
        //-----------------------------------------------------------------------------------------
        // GET: GetEmployeeLIST
        public async Task<ActionResult> GetUsersData()
        {
            var data = await _user.GetAllusers() ?? new List<UsersModel>();

            var filterbyProj = data.Where(res => res.Project_ID == 10);
            if (filterbyProj == null || !filterbyProj.Any())
                return Json(ResultMessageResponce.JsonError("No Data Found", 404, "No Employee data found"), JsonRequestBehavior.AllowGet);

            return Json(ResultMessageResponce.JsonSuccess(filterbyProj), JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> AddNewUsers()
        {
            try
            {
                var obj = new RegisterModel
                {
                    Project_ID = Convert.ToInt32(Request.Form["ProjectID"]),
                    Username = Request.Form["RegUsername"],
                    Password = PasswordHasher.Hashpassword(Request.Form["RegPassword"]),
                    Email = Request.Form["Email"],
                    Role_ID = 2,
                    FullName = Request.Form["FullName"],
                    Employee_ID = Request.Form["Employee_ID"]
                };

                // Checks if the user Exist 
                bool IsExist = await _user.CheckAccountsTable(obj);
                if (IsExist) return JsonError("Username is already created", 500);

                // Insert in the Database
                await _user.CreateNewAccount(obj);

                return JsonCreated(obj, "New Account Created Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }
        //-----------------------------------------------------------------------------------------
        //---------------------------- COMMON METHODS  ----------------------------------------
        //-----------------------------------------------------------------------------------------
        [JwtAuthorize]
        public async Task<ActionResult> GetCategorylist()
        {
            var data = await _partsList.GetPartsMasterlist() ?? new List<MasterlistPartsModel>();

            var filterdata = data.
                GroupBy(x => new { x.CategoryID, x.CategoryName })
                .Select(g => new
                {
                    CategoryID = g.Key.CategoryID,
                    CategoryName = g.Key.CategoryName
                }).ToList();

            if (filterdata == null || !filterdata.Any()) return JsonNotFound("No Category list Data.");

            return JsonSuccess(filterdata, "No Category  List");
        }
        public ActionResult DisplaytheImage(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return HttpNotFound();

            string folderPath = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\HydroParts\";
            string fullPath = Path.Combine(folderPath, filename);

            if (!System.IO.File.Exists(fullPath))
                return File("~/Content/Images/no-image.png", "image/png");

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, "image/png"); // Change to "image/jpeg" if needed
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
        public async Task<ActionResult> GetAllChambers()
        {
            var data = await _chambers.GetAllChambersDisplay() ?? new List<ChamberslistModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Chamber Data.");

            return JsonSuccess(data, "Load Chamber List");
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
        public async Task<ActionResult> GetChamberList()
        {
            var data = await _chambers.GetChamberTypes() ?? new List<ChamberTypeList>();
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

        [HttpPost]
        public async Task<ActionResult> AddChamberparts(AddPartsChamberModel add)
        {
            try
            {
                bool result = await _chambers.AdditionalChambers(add);
                if (!result) return JsonPostError("INSERT failed.", 500);
                return JsonCreated(result, "INSERT new Parts Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }
        }

        //-----------------------------------------------------------------------------------------
        //---------------------------- HYDRO STOCk INVENTORY PAGE ---------------------------------
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult> AddInventoryParts(AddInventoryModel model, HttpPostedFileBase partsimage)
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

            bool result = await _hydro.AddInventory(model);
            if (!result) return JsonPostError("Insert failed.", 500);

            CacheHelper.Remove("MaterialPartlist");

            return JsonCreated(model, "Edit Masterlist Successfully");

        }

        [HttpPost]
        public async Task<ActionResult> EditInventoryParts(HttpPostedFileBase Editpartsimage)
        {
            string fullnamefile = "";
            string exportFolder = @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\HydroParts\";

            // Get existing image name from hidden field (if available)
            string existingImage = Request.Form["ExistingImageParts"];
            string Partno = Request.Form["TempPartno"];

            // CASE 1: If a new image is uploaded
            if (Editpartsimage != null && Editpartsimage.ContentLength > 0)
            {

                if (!Directory.Exists(exportFolder))
                    Directory.CreateDirectory(exportFolder);

            
                string fileName = Path.GetFileNameWithoutExtension(Editpartsimage.FileName);
                string extension = Path.GetExtension(Editpartsimage.FileName);


                // Create unique filename (avoid overwriting existing files)
                fullnamefile = fileName + "_" + Guid.NewGuid().ToString("N").Substring(0, 6) + extension;
                string fullPath = Path.Combine(exportFolder, fullnamefile);


                // Save the uploaded file to disk
                Editpartsimage.SaveAs(fullPath);


                if (!string.IsNullOrEmpty(existingImage))
                {
                    string oldFilePath = Path.Combine(exportFolder, existingImage);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        try
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Error deleting old image: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                // If the item previously had an image, keep it
                if (!string.IsNullOrEmpty(existingImage))
                {
                    fullnamefile = existingImage;
                }
                else
                {
                    // Otherwise, no image is available
                    fullnamefile = "";
                }
            }



            var editmodel = new AddInventoryModel
            {
                PartNo = Request.Form["EditPartNo"],
                PartName = Request.Form["EditPartName"],
                CategoryID = Convert.ToInt32(Request.Form["EditCategoryID"]),
                Supplier = Request.Form["EditSupplier"],
                Unit = Request.Form["EditUnit"],
                Unit_Price = Convert.ToDouble(Request.Form["EditUnit_Price"]),
                WarningLevel = Convert.ToInt32(Request.Form["EditWarningLevel"]),
                ReorderLevel = Convert.ToInt32(Request.Form["EditReorderLevel"]),
                ImageParts = fullnamefile   // Save new or existing filename
            };

            bool result = await _hydro.EditInventory(editmodel, Partno);
            if (!result) return JsonPostError("Insert failed.", 500);

            CacheHelper.Remove("MaterialPartlist");

            return JsonCreated(editmodel, "Edit Masterlist Successfully");

        }

        [JwtAuthorize]
        public async Task<ActionResult> GetHydroInventory()
        {
            var data = await _hydro.GetInventoryList() ?? new List<StockPartsModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Hydro parlist Data.");

            return JsonSuccess(data, "Load Inventory List");
        }

        [JwtAuthorize]
        public async Task<ActionResult> GetAddStockList()
        {
            var data = await _hydro.GetAddStocksList() ?? new List<StockAddModel>();
            if (data == null || !data.Any()) return JsonNotFound("No Hydro parlist Data.");

            return JsonSuccess(data, "Load Inventory List");
        }


        [JwtAuthorize]
        public async Task<ActionResult> GetAddDetailsList(int ID)
        {
            var data = await _hydro.GetAddStocksDetails(ID) ?? new List<StockAddDetailsModel>();
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

        [HttpPost]
        public async Task<ActionResult> SaveStocks()
        {
            try
            {
                var json = Request.Form["items"];

                var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockItem>>(json);

                foreach (var item in items)
                {
                    await _stock.AddStocks(item.PartNo, item.Quantity);
                }

                return JsonCreated(true, "Add Stocks Successfully");
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message, 500);
            }

        }


        [HttpPost]
        public async Task<ActionResult> AddNewStocks()
        {
            try
            {
                //string requestedBy = Request.Form["RequestBy"];   
                //string remarks = Request.Form["Purpose"];
                var json = Request.Form["items"];

                var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AddStocksItem>>(json);

                bool result = await _hydro.AddStockItem(items);

                if (!result) return JsonPostError("Insert failed.", 500);

                return JsonCreated(result, "Add Stocks Successfully");
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
        public async Task<ActionResult> GetAllRequestList(
                    string chamberType = "all",
                    string status = "all",
                    string startDate = "",
                    string endDate = "",
                    int page = 1,
                    int pageSize = 10)
        {
            var allData = await _chambers.GetRequestList() ?? new List<RequestChambersModel>();

            var filtered = allData.AsQueryable();

            // filter by ChamberType
            if (!string.IsNullOrEmpty(chamberType) && chamberType != "all")
                filtered = filtered.Where(x => x.ChamberName == chamberType);
            // filter by Status
            if (!string.IsNullOrEmpty(status) && status != "all")
                filtered = filtered.Where(x => x.RequestStatus == status);
            // filter by Search 
            //if (!string.IsNullOrEmpty(search))
            //    filtered = filtered.Where(x => x.ChamberName.Contains(search) ||
            //                                   x.OrderID.ToString().Contains(search) ||
            //                                   x.PIC.Contains(search));
            // filter by StartDate 
            if (DateTime.TryParse(startDate, out var sDate))
                filtered = filtered.Where(x => DateTime.Parse(x.OrderDate) >= sDate);
            // filter by EndDate 
            if (DateTime.TryParse(endDate, out var eDate))
                filtered = filtered.Where(x => DateTime.Parse(x.TargetDate) <= eDate);



            var total = filtered.Count();
            var paged = filtered
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();


            if (paged == null || !paged.Any()) return JsonNotFound("No Request list Data.");

            return JsonSuccess(new
            {
                Data = paged,
                Total = total
            }, "Load Request List");
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
        public async Task<ActionResult> UpdatesRequestMaterialsV2([System.Web.Http.FromBody] List<AllocationRequest> allo)
        {
            try
            {
                string ID = "";

                foreach (var item in allo)
                {
                    await _chambers.UpdatesRequestMaterials(item.OrderID, item.PartNo, item.allocated);
                    ID = item.OrderID;
                }

                var data = await _chambers.GetRequestList() ?? new List<RequestChambersModel>();
                double completionrate = data.SingleOrDefault(res => res.OrderID == ID).CompletionPercent;

                if(completionrate == 100)
                {
                    await _chambers.UpdateRequestStatus(ID, "Completed");
                } 


                return Json(new { Success = true, Message = "Allocations saved successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }

        }


        [HttpPost]
        public async Task<ActionResult> ChangeStatusRequest(string OrderID, string Status)
        {
            try
            {
                bool result = await _chambers.UpdateRequestStatus(OrderID, Status);
                if (!result) return JsonPostError("Update failed.", 500);


                return JsonCreated(result, "Update Status Successfully");
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
        public ActionResult Chambers() =>  View();
        // GET: Hydroponics/OrderpageDetails
        public ActionResult ChambersDetails(int chamberID)
        {
            if (chamberID == 0)
            {
                return RedirectToAction("Chambers", "Hydro", new { area = "Hydroponics" });
            }
            return View();
        }
        // GET: Hydroponics/PartList
        public ActionResult PartList() => View();
        // GET: Hydroponics/RequestStock
        public ActionResult RequestStock() => View();
        // GET: Hydroponics/UserManage
        public ActionResult TransactionHistory() => View();
        // GET: Hydroponics/UserManage
        public ActionResult UserManage() => View();
        // GET: Hydroponics/PartList
        public ActionResult SampleLayout() => View();
    }
}