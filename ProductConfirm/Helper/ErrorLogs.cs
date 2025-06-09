using System;
using System.IO;
using System.Windows.Forms;

namespace ProductConfirm.Helper
{
    public static class ErrorLogs
    {
        public static void SendToLogs(string method, Exception ex)
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ErrorLog.txt");
            string errorMessage = $"[{DateTime.Now}] Method: {method} - Exception: {ex.Message}\nStackTrace: {ex.StackTrace}\n";

            try
            {
                File.AppendAllText(logFilePath, method);
            }
            catch
            {
                MessageBox.Show("Failed to write to the error log file.", "Logging Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
