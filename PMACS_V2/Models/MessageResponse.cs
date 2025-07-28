
using System.Collections.Generic;

namespace PMACS_V2.Models
{
    public class DataMessageResponse<T>
    {
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