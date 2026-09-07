using ProgramPartListWeb.Areas.PC.Interface;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Controllers;
using ProgramPartListWeb.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProgramPartListWeb.Areas.PC.Controllers
{
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

        [HttpPost]
        public ActionResult SendEmailV2(EmailModel model)
        {
            bool result = EmailService.SendEmail(
                "jaye.labandia@sanyodenki.com",
                "Reminder: Due Date Approaching",
                "<h2>Hello!</h2><p>This is your reminder email.</p>"
            );

            return Json(new { success = result, message = result ? "Email sent." : "Failed to send email." });
        }

        [HttpPost]
        public JsonResult SendEmail(EmailModel model)
        {
            bool result = EmailService.SendEmail(
                "jaye.labandia@sanyodenki.com",
                "Reminder: Due Date Approaching",
                "<h2>Hello!</h2><p>This is your reminder email.</p>"
            );
            return Json(new { success = result, message = result ? "Email sent." : "Failed to send email." });
        }
    }
}