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
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true)]
        static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter")]
        static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true)]
        static extern bool StartDocPrinter(IntPtr hPrinter, int level, ref DOCINFOA pDocInfo);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter")]
        static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter")]
        static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter")]
        static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true)]
        static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;

            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        public static bool SendZplToPrinter(string printerName, string zpl)
        {
            IntPtr hPrinter;
            DOCINFOA di = new DOCINFOA
            {
                pDocName = "ZPL Label",
                pDataType = "RAW"
            };

            bool success = false;

            if (OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
            {
                if (StartDocPrinter(hPrinter, 1, ref di))
                {
                    if (StartPagePrinter(hPrinter))
                    {
                        IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(zpl);
                        success = WritePrinter(hPrinter, pBytes, zpl.Length, out int bytesWritten);
                        Marshal.FreeCoTaskMem(pBytes);

                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }

            return success;
        }


        //public static bool SendZplToPrinter(string printerName, string zpl)
        //{
        //    IntPtr hPrinter;
        //    DOCINFOA di = new DOCINFOA
        //    {
        //        pDocName = "ZPL Label",
        //        pDataType = "RAW"
        //    };

        //    if (OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
        //    {
        //        if (StartDocPrinter(hPrinter, 1, ref di))
        //        {
        //            StartPagePrinter(hPrinter);

        //            IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(zpl);
        //            WritePrinter(hPrinter, pBytes, zpl.Length, out _);
        //            Marshal.FreeCoTaskMem(pBytes);

        //            EndPagePrinter(hPrinter);
        //            EndDocPrinter(hPrinter);
        //        }
        //        ClosePrinter(hPrinter);
        //        return true;
        //    }
        //    return false;
        //}

        public static string GenerateZPLFromList(List<FinalLabelData> dataList)
        {
            StringBuilder zplBuilder = new StringBuilder();

            // Start ZPL
            zplBuilder.AppendLine("^XA");

            foreach (var data in dataList)
            {
                zplBuilder.AppendLine("^FO20,20^A0N,40,40^FD" + data.SDP + "^FS");
                zplBuilder.AppendLine("^FO20,80^A0N,30,30^FD" + data.Warehouse + "^FS");
                zplBuilder.AppendLine("^FO20,120^A0N,30,30^FD" + data.Quantity + "^FS");
                // Add more ^FO / ^FD fields based on your layout and `FinalLabelData` fields

                zplBuilder.AppendLine("^XZ"); // Close current label
                zplBuilder.AppendLine("^XA"); // Open a new label for next item
            }

            zplBuilder.AppendLine("^XZ"); // Final close
            return zplBuilder.ToString();
        }

        // TESTING NOW
        public static string GenerateMultiLabelZplV2(List<(string SDP, string Warehouse, string Quantity)> labels)
        {
            StringBuilder zpl = new StringBuilder();

            // ZPL start
            zpl.AppendLine("^XA");

            foreach (var item in labels)
            {
                // --- Label header ---
                zpl.AppendLine("^CF0,15"); // Default font
                zpl.AppendLine("^FO30,20^FD" + item.SDP + "^FS"); // Part number text

                // --- QR Code ---
                zpl.AppendLine("^FO380,0");
                zpl.AppendLine("^BQN,2,2");
                zpl.AppendLine("^FDLA," + item.SDP + "^FS");

                // --- Barcode (for SDP) ---
                zpl.AppendLine("^FO30,40");
                zpl.AppendLine("^BY1.2,2,10"); // Narrower barcode width (was ^BY2)
                zpl.AppendLine("^BCN,50,N,N,N"); // HRI removed
                zpl.AppendLine("^FD" + item.SDP + "^FS");

                // --- Warehouse text ---
                zpl.AppendLine("^CF0,15");
                zpl.AppendLine("^FO30,110^FDSMT WH : " + item.Warehouse + "^FS");

                // --- Barcode (for warehouse) ---
                zpl.AppendLine("^FO30,130");
                zpl.AppendLine("^BY1.2,2,10"); // Default width retained for Warehouse
                zpl.AppendLine("^B3N,N,50,N,N");
                zpl.AppendLine("^FD" + item.Warehouse + "^FS");

                // --- Quantity Text ---
                zpl.AppendLine("^CF0,15");
                zpl.AppendLine("^FO300,110^FDQty:0" + item.Quantity + "^FS");

                // --- Quantity Barcode ---
                zpl.AppendLine("^FO300,130");
                zpl.AppendLine("^BY1,2,50");       // <== This line controls the barcode width
                zpl.AppendLine("^BCN,50,N,N,N");
                zpl.AppendLine("^FD0" + item.Quantity + "^FS");

                // --- Label separator ---
                zpl.AppendLine("^XZ"); // End label
                zpl.AppendLine("^XA"); // Start next label
            }

            zpl.AppendLine("^XZ"); // Final end
            return zpl.ToString();
        }

        public static string GenerateMultiLabelZpl(List<(string SDP, string Warehouse, string Quantity)> labels)
        {
            StringBuilder zpl = new StringBuilder();

            zpl.AppendLine("^XA"); // Start of ZPL

            int labelHeight = 170; // Vertical space between labels
            int index = 0;

            foreach (var item in labels)
            {
                int yOffset = index * labelHeight;

                // --- Header Text ---
                zpl.AppendLine($"^CF0,15");
                zpl.AppendLine($"^FO30,{20 + yOffset}^FD{item.SDP}^FS");

                // --- QR Code ---
                zpl.AppendLine($"^FO380,{0 + yOffset}");
                zpl.AppendLine("^BQN,2,2");
                zpl.AppendLine($"^FDLA,{item.SDP}^FS");

                // --- SDP Barcode ---
                zpl.AppendLine($"^FO30,{40 + yOffset}");
                zpl.AppendLine("^BY1.2,2,10");
                zpl.AppendLine("^BCN,50,N,N,N");
                zpl.AppendLine($"^FD{item.SDP}^FS");

                // --- Warehouse Text ---
                zpl.AppendLine("^CF0,15");
                zpl.AppendLine($"^FO30,{110 + yOffset}^FDSMT WH : {item.Warehouse}^FS");

                // --- Warehouse Barcode ---
                zpl.AppendLine($"^FO30,{130 + yOffset}");
                zpl.AppendLine("^BY1.2,2,10");
                zpl.AppendLine("^B3N,N,50,N,N");
                zpl.AppendLine($"^FD{item.Warehouse}^FS");

                // --- Quantity Text ---
                zpl.AppendLine("^CF0,15");
                zpl.AppendLine($"^FO300,{110 + yOffset}^FDQty:0{item.Quantity}^FS");

                // --- Quantity Barcode ---
                zpl.AppendLine($"^FO300,{130 + yOffset}");
                zpl.AppendLine("^BY1,2,50");
                zpl.AppendLine("^BCN,50,N,N,N");
                zpl.AppendLine($"^FD0{item.Quantity}^FS");

                index++;

                // Print only 2 per page
                if (index % 2 == 0)
                {
                    zpl.AppendLine("^XZ^XA"); // End current ZPL page and start new one
                    index = 0;
                }
            }

            zpl.AppendLine("^XZ"); // Final ZPL end

            return zpl.ToString();
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
                        if (name.Equals("ZDesigner ZD421-203dpi ZPL", StringComparison.OrdinalIgnoreCase)
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
