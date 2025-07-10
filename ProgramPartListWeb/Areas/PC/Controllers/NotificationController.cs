using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Utilities;
using ProgramPartListWeb.Utilities.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.PC.Controllers
{
    [CompressResponse]
    public class NotificationController : ExtendController
    {
        private readonly INotification _not;
        public NotificationController(INotification not) => _not = not;

        public async Task<ActionResult> GetNotificationList()
        {
            var data = await _not.GetUserNotifications() ?? new List<Notification>();
            //var res = CacheHelper.GetOrSet("Pressmasterlist", () => product, 15);
            if (data == null)
                return JsonNotFound("No Notification");

            return JsonSuccess(data);
        }


    }
}