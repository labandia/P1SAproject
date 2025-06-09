using Newtonsoft.Json;
using System;
using System.IO;

namespace ProductConfirm.Utilities
{
    public static class CustomLogger
    {
        // Primary network log folder
        private static readonly string networkLogFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\logs";
        private static readonly string networkLogFilePath = Path.Combine(networkLogFolder, "error_log.txt");

        // Fallback local log folder
        private static readonly string localLogFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
        private static readonly string localLogFilePath = Path.Combine(localLogFolder, "error_log.txt");

        public static void LogError(Exception ex)
        {
            try
            {
                // Attempt to write to network location
                WriteLog(networkLogFilePath, networkLogFolder, ex);
            }
            catch (Exception netEx)
            {
                try
                {
                    // If network logging fails, write to local log
                    WriteLog(localLogFilePath, localLogFolder, ex);
                }
                catch (Exception localEx)
                {
                    // If local logging also fails, last resort: console output
                    Console.WriteLine("Failed to log error to both network and local storage.");
                    Console.WriteLine("Network log error: " + netEx.Message);
                    Console.WriteLine("Local log error: " + localEx.Message);
                }
            }
        }

        private static void WriteLog(string filePath, string folderPath, Exception ex)
        {
            // Ensure directory exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Create log entry
            var logEntry = new
            {
                Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source,
                InnerException = ex.InnerException?.Message
            };

            // Convert to JSON format
            string jsonLog = JsonConvert.SerializeObject(logEntry, Newtonsoft.Json.Formatting.Indented);

            // Append to log file
            File.AppendAllText(filePath, jsonLog + Environment.NewLine);
        }
    }
}
