
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
}