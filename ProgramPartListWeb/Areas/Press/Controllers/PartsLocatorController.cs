using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Areas.Press.Interfaces;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;  
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Press.Controllers
{
    [GlobalErrorException]
    public class PartsLocatorController : ExtendController
    {
        private readonly IUserRepository _user;
        private readonly IAluminumProducts _press;

        public PartsLocatorController(IAluminumProducts press, IUserRepository user)
        {
            _press = press;
            _user = user;
        }


        //---------------- GET DATA -----------------------------
        [HttpGet]
        [CompressResponse]
        public Task<ActionResult> GetUsersinfo()
        {
            //string fullname = await _user.UsersFullname(userid);
            return Task.FromResult<ActionResult>(Json(Session["Fullname"], JsonRequestBehavior.AllowGet));
        }

        // GET: GetPressMasterList
        [HttpGet]
        [CompressResponse]
        public async Task<ActionResult> GetPressMasterList()
        {
            try
            {
                var data = await _press.GetPressMasterData() ?? new List<PressMasterlistModel>();
                var product = data.Where(p => p.Storage_ID != 0).ToList();
                if (product == null || !product.Any())
                    return JsonNotFound("No Masterlist data found");

                return JsonSuccess(product);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        [HttpGet]
        [CompressResponse]
        public async Task<ActionResult> GetMasterlistInfo(int ID)
        {
            try
            {
                var data = await _press.GetPressMasterData() ?? new List<PressMasterlistModel>();
                var product = data.FirstOrDefault(p => p.Storage_ID == ID);
                //var res = CacheHelper.GetOrSet("Pressmasterlistinfo", () => product, 15);
                if (data == null || !data.Any())
                    return JsonNotFound("No Master list Details data found");

                return JsonSuccess(product);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }    
        }


        [HttpGet]
        [CompressResponse]
        public async Task<ActionResult> GetStorageRacks(int? rackID, string parts)
        {
            try
            {
                var product = new List<PressMasterlistModel>();
                var data = await _press.GetPressMasterData();
                if (string.IsNullOrEmpty(parts))
                {
                    product = data.Where(p => p.Racksnum == rackID).ToList();
                }
                else
                {
                    product = data.Where(p => p.Racksnum == rackID && p.Partnum == parts).ToList();
                }

                if (product == null || !product.Any())
                    return JsonNotFound("No Storage Racks data found");

                return JsonSuccess(product);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }   
        }


        [HttpGet]
        [CompressResponse]
        public async Task<ActionResult> GetTransactionHistory(int act)
        {
            try
            {
                var data = await _press.GetPressHistoryTransactionData(act);
                if (data == null || !data.Any())
                    return JsonNotFound("No History Transaction data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }

        [HttpGet]
        [CompressResponse]
        public async Task<ActionResult> GetIssuanceHistoryData()
        {
            try {
                var data = await _press.GetIssuanceHistory();
                if (data == null || !data.Any())
                    return JsonNotFound("No Issuance History  found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
          
        }



        [HttpGet]
        [CompressResponse]
        public async Task<ActionResult> GetIDNoteColor()
        {
            try
            {
                var data = await _press.GetIDnoteData();
                if (data == null || !data.Any())
                    return JsonNotFound("No Issuance History  found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }



        [HttpPost]
        public async Task<ActionResult> EditpressMasterlist()
        {
            int mastID = Convert.ToInt32(Request.Form["Master_ID"]);
            int note = Convert.ToInt32(Request.Form["NoteID"]);

            var setnewval = new
            {
                Storage_ID = Convert.ToInt32(Request.Form["StorageID"]),
                Quantity = Convert.ToInt32(Request.Form["Quantity"]),
                Postnum = Request.Form["Postnum"],
                Racksnum = Convert.ToInt32(Request.Form["Racks"])
            };
     

            bool result = await _press.UpdateMasterlistData(setnewval, mastID, note);
            CacheHelper.Remove("Pressmasterlist");
            var formdata = GlobalUtilities.GetMessageResponse(result, 1);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> AddNewStocksIn(int AddQuan, int Storage_ID)
        {
   
            bool result = await _press.UpdateStorageData(Storage_ID, AddQuan);
            var formdata = GlobalUtilities.GetMessageResponse(result, 1);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Issuanceprocess()
        {
            int issuedID = Convert.ToInt32(Request.Form["IssuanceID"]);
            int Quan = Convert.ToInt32(Request.Form["IssuedQuan"]);

            var setnewval = new
            {
                IssuanceID = issuedID,
                IssuedQuan = Quan,
                IssuedBy = Request.Form["IssuedBy"]
            };

         
            bool result = await _press.UpdateIssuance(setnewval);

            var formdata = GlobalUtilities.GetMessageResponse(result, 1);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]
        public async Task<ActionResult> IssuanceHistorySummary()
        {
            int newQuan = Convert.ToInt32(Request.Form["NewQuan"]);
            int StorageID = Convert.ToInt32(Request.Form["Storage_ID"]);

            var setnewval = new
            {
                FA_Shoporder = Request.Form["FA_Shoporder"],
                FA_Plan = Request.Form["FA_Plan"],
                Storage_ID = StorageID,
                Received = Request.Form["Received"]
            };

            bool result = await _press.UpdateStorageData(StorageID, newQuan);

            if (result)
            {
                await _press.InsertSummaryData(setnewval);
            }

            var formdata = GlobalUtilities.GetMessageResponse(result, 1);

            return Json(formdata, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> AddMasterlistData(AddPressMasterlistModel obj)
        {
            bool result = await _press.AddNewProducts(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public  ActionResult ChangepasswordEdit()
        {
            var setnewval = new
            {
                currentpass = Request.Form["currentpassword"],
                newpass = Request.Form["newpassword"],
                confirmpass = Request.Form["confirmwrong"]
            };

            return Json(setnewval, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult LogOut()
        {
          
            Session.Clear();
            return View();
        }




        // GET: Press/PartsLocator
        [CompressResponse]
        public ActionResult Home(){ return View();}
        //[JwtAuthorizationFilter]
        [CompressResponse]
        public ActionResult StorageLocation() { return View(); }
        //[JwtAuthorizationFilter]
        [CompressResponse]
        public ActionResult Masterlist() { return View(); }
        //[JwtAuthorizationFilter]
        [CompressResponse]
        public ActionResult HistoryTransaction() { return View(); }
        [CompressResponse]
        public ActionResult IssuanceParts() { return View(); }
        [CompressResponse]
        public ActionResult HoursSummary() { return View(); }
        [CompressResponse]
        public ActionResult Changepassword() { return View(); }
        [CompressResponse]
        public ActionResult ProductDetails(int StorageID) {
            ViewBag.Storage_ID = StorageID;
            return View(); 
        }

    }

}