using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Management;
using System.Net.Sockets;


namespace ZebraPrinterLabel
{
    public sealed class ZebraProcess
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool OpenPrinter(string pPrinterName, out IntPtr hPrinter, IntPtr pDefault);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In] ref DOCINFOA di);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", SetLastError = true)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
        }

        public static bool SendZplToPrinter(string printerName, string zpl)
        {
            IntPtr hPrinter;
            DOCINFOA di = new DOCINFOA
            {
                pDocName = "ZPL Label",
                pDataType = "RAW"
            };

            if (OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
            {
                if (StartDocPrinter(hPrinter, 1, ref di))
                {
                    StartPagePrinter(hPrinter);

                    IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(zpl);
                    WritePrinter(hPrinter, pBytes, zpl.Length, out _);
                    Marshal.FreeCoTaskMem(pBytes);

                    EndPagePrinter(hPrinter);
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
                return true;
            }
            return false;
        }



        public static bool ZebraPrinterConnected()
        {
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer.ToLower().Contains("zebra"))
                {
                    PrinterSettings settings = new PrinterSettings
                    {
                         PrinterName = printer,
                    };

                    if (settings.IsValid)
                    {
                        return true;
                    }
                }
            }

            return false; // No Zebra printer found
        }

        public async static Task<bool> IsZebraPrinterConnectedViaUSB()
        {
            return await Task.Run(() =>
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
                {
                    foreach (ManagementObject printer in searcher.Get())
                    {
                        string name = printer["Name"]?.ToString() ?? "";
                        string port = printer["PortName"]?.ToString() ?? "";

                        if (name.Equals("EPSON L3210 Series", StringComparison.OrdinalIgnoreCase) &&
                            port.ToLower().Contains("usb"))
                        {
                            return true;
                        }
                    }
                }
                return false;
            });
        }
        public static bool IsZebraPrinterOnline(string ipAddress, int port = 9100)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    var result = client.BeginConnect(ipAddress, port, null, null);
                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));
                    if (!success)
                        return false;

                    client.EndConnect(result);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        public async static Task<bool> IsZebraPrinterConnectedAndOnline()
        {
            return await Task.Run(() =>
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
                {
                    foreach (ManagementObject printer in searcher.Get())
                    {
                        string name = printer["Name"]?.ToString() ?? "";
                        string port = printer["PortName"]?.ToString() ?? "";
                        bool isOffline = Convert.ToBoolean(printer["WorkOffline"] ?? false);

                        // Check for exact name match and USB port, and ensure it's online
                        if (name.Equals("EPSON L3210 Series", StringComparison.OrdinalIgnoreCase)
                            && port.StartsWith("USB", StringComparison.OrdinalIgnoreCase)
                            && !isOffline)
                        {
                            return true;
                        }
                    }
                }
                return false;
            });
        }

    }
}
