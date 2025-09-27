using ProgramPartListWeb.Areas.Hydroponics.Interface;
using ProgramPartListWeb.Areas.Hydroponics.Models;
using ProgramPartListWeb.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.Hydroponics.Controllers
{
    public class StockController : ExtendController
    {
        public readonly IStockAlertService _stock;

        public StockController(IStockAlertService stock)
        {
            _stock = stock;
        }

        // FOR NOTIFICATION ALERTS IN THE SYSTEM
        [HttpGet]
        public async Task<JsonResult> GetStockAlerts()
        {
            var data = await _stock.GetActiveAlertsAsync() ?? new List<StockAlert>();

            return Json(new
            {
                Success = true,
                Alerts = data,
                LastCheck = DateTime.Now
            }, JsonRequestBehavior.AllowGet);
        }


        // FOR SENDING EMAIL NOTIFICATION
        [HttpPost]
        public async Task<ActionResult> StocksAlertEmailNotification()
        {
            bool sent = await _stock.SendEmailNotificationStocks();
            Debug.WriteLine("SDSa" + sent);
            if (!sent) return JsonPostError("Update failed.", 500);
            return JsonCreated(sent, "Update Successfully");
        }

    }
}