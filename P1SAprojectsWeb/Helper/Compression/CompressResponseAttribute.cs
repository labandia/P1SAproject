using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Utilities
{
    public class CompressResponseAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpResponseBase response = filterContext.HttpContext.Response;

            string acceptEncoding = request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToLowerInvariant();

            if (acceptEncoding.Contains("gzip"))
            {
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                response.AppendHeader("Content-Encoding", "gzip");
            }
            else if (acceptEncoding.Contains("deflate"))
            {
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                response.AppendHeader("Content-Encoding", "deflate");
            }

            // Optional: prevent proxy caching of compressed content
            response.AppendHeader("Vary", "Accept-Encoding");
        }
    }
}