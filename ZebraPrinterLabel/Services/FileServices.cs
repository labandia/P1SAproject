using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ZebraPrinterLabel.Services
{
    public sealed class FileServices
    {
        public static string filepath = @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\TempData\ZebraCounter.txt";
        public static CountToday GetReelIDCount()
        {
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);

                try
                {
                    CountToday c = JsonSerializer.Deserialize<CountToday>(json);
                    return c;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Invalid JSON " + ex.Message);
                    return null;
                }
            }
            else
            {
                Debug.WriteLine("File Not Found ");
                return null;
            }
        }


        public static void UpdateCountToday(int newCount)
        {
            if (File.Exists(filepath))
            {         
                try
                {
                    string json = File.ReadAllText(filepath);
                    CountToday obj = JsonSerializer.Deserialize<CountToday>(json);

                    if (obj != null)
                    {
                        obj.Count = newCount;
                        string updateJson = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText(filepath, updateJson);
                        MessageBox.Show("Count updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error " + ex.Message);
                }
            }
        }

        public static void ResetCountPrinter()
        {
            DateTime now = DateTime.Now;
            string today = now.ToString("yyyy-MM-dd");

            // Only run if time is exactly 2:30 AM
            if (now.Hour == 2 && now.Minute  == 30)
            {
                if (File.Exists(filepath))
                {
                    try
                    {
                        string json = File.ReadAllText(filepath);  
                        CountToday obj = JsonSerializer.Deserialize<CountToday>(json);

                        if (obj != null)
                        {
                            // Only Reset if the date is Different
                            if (obj.DateStart != today)
                            {
                                obj.DateStart = today;
                                obj.Count = 0;

                                string updatedJson = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
                                File.WriteAllText(filepath, updatedJson);
                                // Optional: log or notify
                                Debug.WriteLine("Count reset at 2:30 AM.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error " + ex.Message);
                    }
                }
            }
        }
    }
}
