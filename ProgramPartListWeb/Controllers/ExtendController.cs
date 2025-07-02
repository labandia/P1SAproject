using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NLog;
using ProgramPartListWeb.Models;
using ProgramPartListWeb.Utilities;

namespace ProgramPartListWeb.Controllers
{
    public class ExtendController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected JsonResult JsonSuccess(object data = null, string message = "Success", int statusCode = 200)
        {
            Response.StatusCode = statusCode;
            
            // Track the Input of the users
            Logger.Info("{0} | StatusCode={1}{2}",
            message,
            statusCode);

            return Json(new
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = data
            }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonMultipleData(Dictionary<string, IEnumerable<object>> dataSets, string message = null)
        {
            bool isEmpty = dataSets == null || dataSets.Count == 0 || dataSets.All(kvp =>
                kvp.Value == null || !kvp.Value.Any());

            int statusCode = isEmpty ? 404 : 200;
            string responseMessage = message ?? (isEmpty ? "Data not found" : "Data retrieved successfully");
            bool success = !isEmpty;

            var responseData = new Dictionary<string, object>();
            int totalCount = 0;

            if (dataSets != null)
            {
                foreach (var kvp in dataSets)
                {
                    var list = kvp.Value?.ToList() ?? new List<object>();
                    responseData[kvp.Key] = list;
                    totalCount += list.Count;
                }
            }

            var response = new
            {
                Success = success,
                StatusCode = statusCode,
                Message = responseMessage,
                Data = responseData,
                TotalCount = totalCount
            };

            Response.StatusCode = statusCode;

            Logger.Info("{0} | StatusCode={1} | TotalItems={2}", responseMessage, statusCode, totalCount);

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonError(string message = "An error occurred", int statusCode = 500)
        {
            Response.StatusCode = statusCode;
            Response.ContentType = "application/json";

            //Logger.Error("Error Response | StatusCode={0} | Message={1}", statusCode, message);


            return Json(new
            {
                Success = false,
                StatusCode = statusCode,
                Message = message
            }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonValidationError(string message = "Validation failed")
        {
            //Logger.Warn("Validation Error | Message={0}", message);
            return JsonError(message, 400);
        }

        protected JsonResult JsonNotFound(string message = "Resource not found")
        {
            //Logger.Warn("Not Found | Message={0}", message);
            return JsonError(message, 404);
        }

        protected JsonResult JsonConflict(string message = "Conflict detected")
        {
            //Logger.Warn("Conflict | Message={0}", message);
            return JsonError(message, 409);
        }

        protected JsonResult JsonCreated(object data = null, string message = "Created")
        {
            //Logger.Info("Resource Created | Message={0}{1}", message, data != null ? $" | Data: {Newtonsoft.Json.JsonConvert.SerializeObject(data)}" : "");
            return JsonSuccess(data, message, 201);
        }

        protected ActionResult Problem(int status, string title, string detail)
        {
            var problem = new ProblemDetails
            {
                Title = title,
                Status = status,
                Detail = detail,
                Instance = Request?.Url?.AbsoluteUri
            };

            return new ProblemDetailsActionResult(problem);
        }
    }
}