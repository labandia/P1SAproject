
using System;
using System.Collections.Generic;

namespace PMACS_V2.Models
{
    public class DataMessageResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public class MessageResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class Messagepartnum<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Header { get; set; }
        public T Data { get; set; }
    }

    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public int TotalPages =>
            (int)Math.Ceiling((double)TotalRecords / PageSize);
    }


    public class ProblemDetails
    {
        public string Type { get; set; } = "about:blank";
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public IDictionary<string, object> Extensions { get; set; } = new Dictionary<string, object>();
    }
}