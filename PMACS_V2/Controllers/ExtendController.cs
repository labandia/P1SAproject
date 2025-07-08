using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PMACS_V2.Controllers
{
    public class ExtendController : Controller
    {
        protected JsonResult JsonPostError(string message = "An error occurred", int statusCode = 500, string errorCode = null, object extra = null)
        {
            Response.StatusCode = statusCode;

            var errorResponse = new
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                ErrorCode = errorCode,
                Timestamp = DateTime.UtcNow.ToString("o"),
                Path = Request?.Url?.AbsolutePath,
                Extra = extra
            };

            return Json(errorResponse, JsonRequestBehavior.DenyGet);
        }


        protected JsonResult JsonPostSuccess(object data = null, string message = "Success", int statusCode = 200)
        {
            Response.StatusCode = statusCode;
            return Json(new
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = data
            }, JsonRequestBehavior.DenyGet);
        }


        protected JsonResult JsonSuccess(object data = null, string message = "Success", int statusCode = 200)
        {
            Response.StatusCode = statusCode;
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
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonError(string message = "An error occurred", int statusCode = 500)
        {
            Response.StatusCode = statusCode;
            return Json(new
            {
                Success = false,
                StatusCode = statusCode,
                Message = message
            }, JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonValidationError(string message = "Validation failed")
        {
            return JsonError(message, 400);
        }

        protected JsonResult JsonNotFound(string message = "Resource not found")
        {
            return JsonError(message, 404);
        }

        protected JsonResult JsonConflict(string message = "Conflict detected")
        {
            return JsonError(message, 409);
        }

        protected JsonResult JsonCreated(object data = null, string message = "Created")
        {
            return JsonSuccess(data, message, 201);
        }
    }
}