using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Utilities.Common
{
    public sealed class ResultMessageResponce
    {
        public static DataMessageResponse<object> JsonSuccess(object data = null, string message = "Success", int statusCode = 200)
        {
            return new DataMessageResponse<object>
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }

        public static DataMessageResponse<object> JsonCreated(object data = null, string message = "Created")
        {
            return JsonSuccess(data, message, 201);
        }

        public static RFCDataMessageResponse<object> JsonError(string title = "An error occurred", int statusCode = 500, string detail = null, string instance = null)
        {
            return new RFCDataMessageResponse<object>
            {
                Success = false,
                StatusCode = statusCode,
                Title = title,
                Detail = detail,
                Instance = instance ?? GetRequestUrl(),
                Message = detail
            };
        }


        private static string GetRequestUrl()
        {
            try
            {
                var context = HttpContext.Current;
                return context?.Request?.Url?.AbsoluteUri;
            }
            catch
            {
                return null;
            }
        }



    }
}