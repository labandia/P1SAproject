using Microsoft.AspNet.SignalR;
using ProgramPartListWeb.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult CheckStock()
        {
            // Simulated low-stock items
            var lowStockItems = new[]
            {
            new { Name = "Part A", Quantity = 2 },
            new { Name = "Part B", Quantity = 0 }
        };

            var context = GlobalHost.ConnectionManager.GetHubContext<StockHub>();

            foreach (var item in lowStockItems)
            {
                if (item.Quantity <= 2) // threshold
                {
                    context.Clients.All.receiveStockAlert(item.Name, item.Quantity);
                }
            }

            return Content("Stock alerts sent");
        }
    }
}