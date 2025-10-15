using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Hydroponics.Controllers
{
    public class StockController : ExtendController
    {
        public readonly IStockAlertService _stock;
        private readonly IUserRepository _user;

        public StockController(IStockAlertService stock, IUserRepository user)
        {
            _stock = stock;
            _user = user;
        }

        [HttpGet]
        public async Task<JsonResult> GetNoficationDisplay(int userID)
        {
            var data = await _stock.GetLowStockNotificationList(userID);
            if (data == null || !data.Any())
                return Json(ResultMessageResponce.JsonError("No notification Found", 404, "No notification data found"), JsonRequestBehavior.AllowGet);

            return Json(ResultMessageResponce.JsonSuccess(data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetNoficationDetailsDisplay(int Notify, int userID)
        {
            var data = await _stock.GetNotificationDetails(Notify, userID);
            if (data == null || !data.Any())
                return Json(ResultMessageResponce.JsonError("No notification Detials Found", 404, "No notification Details data found"), JsonRequestBehavior.AllowGet);

            return Json(ResultMessageResponce.JsonSuccess(data), JsonRequestBehavior.AllowGet);
        }



        // FOR NOTIFICATION ALERTS IN THE SYSTEM
        [HttpGet]
        public async Task<JsonResult> GetStockAlerts()
        {
            try
            {
                var getuserdata = await _user.GetAllusers() ?? new List<UsersModel>();

                var userIDs = getuserdata
                                    .Where(res => res.Project_ID == 10)
                                    .Select(res => res.User_ID)
                                    .ToList();
                int result = await _stock.GenerateStockNotification(userIDs);

                if (result == 0)
                {
                    return Json(new
                    {
                        Success = false,
                        Message = "No notification generated",
                        LastCheck = DateTime.Now
                    }, JsonRequestBehavior.AllowGet);
                }

                //var data = await _stock.GetActiveAlertsAsync() ?? new List<StockAlert>();

                return Json(new
                {
                    Success = result,
                    Alerts = "",
                    LastCheck = DateTime.Now
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                // 🔴 Never crash the app, return error safely
                return Json(new
                {
                    Success = false,
                    Message = "Error while fetching stock alerts",
                    Error = ex.Message,
                    LastCheck = DateTime.Now
                }, JsonRequestBehavior.AllowGet);
            }
           
        }


        // FOR SENDING EMAIL NOTIFICATION
        [HttpPost]
        public async Task<ActionResult> StocksAlertEmailNotification()
        {
            try
            {
                bool sent = await _stock.SendEmailNotificationStocks();
                return JsonCreated(sent, "Update Successfully");
            }catch(Exception e)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Error while sending Email stock alerts",
                    Error = e.Message,
                    LastCheck = DateTime.Now
                }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}