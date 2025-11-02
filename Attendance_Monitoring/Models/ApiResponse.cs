using System.Collections.Generic;

namespace Attendance_Monitoring.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public List<T> Payload { get; set; }
        public string Message { get; set; }

        public ApiResponse()
        {
            Payload = new List<T>();
        }
    }
}
