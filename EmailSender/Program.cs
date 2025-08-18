using EmailSender.Repository;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace EmailSender
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;

        private static async Task Main(string[] args)
        {
            // Hide the console window (optional if Output Type is Console)
            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            while (true)
            {
                try
                {
                    // Check and send emails
                    await EmailRepository.EmailSentProcess();
                }
                catch (Exception ex)
                {
                    // Optional: log exception to file instead of console
                    System.IO.File.AppendAllText("error.log", $"{DateTime.Now}: {ex}{Environment.NewLine}");
                }

                await Task.Delay(5000); // Wait for 5 seconds asynchronously
            }
        }
    }
}
