using System;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace ProgramPartListWeb.Utilities.Security
{
    public class RateLimiting : ActionFilterAttribute
    {
        private readonly int _limit;
        private readonly TimeSpan _window;
        private static readonly ObjectCache _cache = MemoryCache.Default;
        private static readonly object _lock = new object();

        public RateLimiting(int limit, int windowInMinutes)
        {
            _limit = limit;
            _window = TimeSpan.FromMinutes(windowInMinutes);
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ip = HttpContext.Current?.Request?.UserHostAddress ?? "unknown";
            var key = $"{ip}_{filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}_{filterContext.ActionDescriptor.ActionName}";

            lock (_lock)
            {
                var entry = _cache.Get(key) as RateLimitEntry;

                if (entry == null)
                {
                    entry = new RateLimitEntry { Count = 1 };
                    _cache.Add(key, entry, DateTimeOffset.UtcNow.Add(_window));
                }
                else
                {
                    entry.Count++;
                    if (entry.Count > _limit)
                    {
                        filterContext.Result = new ContentResult
                        {
                            Content = "Too many requests. Rate limit exceeded.",
                            ContentType = "text/plain"
                        };
                        filterContext.HttpContext.Response.StatusCode = 429; // Too Many Requests
                    }
                }
            }

        }

        private class RateLimitEntry
        {
            public int Count { get; set; }
        }


    }
}