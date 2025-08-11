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
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error " + ex.Message);
                }
            }
        }
        public static async Task ResetCountPrinterAsync()
        {
            DateTime now = DateTime.Now;
            string today = now.ToString("yyyy-MM-dd");

            if (!File.Exists(filepath))
            {
                Debug.WriteLine($"File not found: {filepath}");
                return;
            }

            try
            {
                string json;
                using (var reader = new StreamReader(filepath))
                {
                    json = await reader.ReadToEndAsync();
                }

                CountToday obj = JsonSerializer.Deserialize<CountToday>(json);

                if (obj == null)
                {
                    Debug.WriteLine("Deserialized object is null.");
                    return;
                }

                if (!DateTime.TryParse(obj.DateStart, out DateTime savedDate))
                {
                    Debug.WriteLine("Failed to parse saved date. Resetting to today.");
                    obj.DateStart = today;
                    obj.Count = 0;
                }
                else
                {
                    // Check if savedDate is before today and time is past 2:30 AM
                    if (savedDate.Date < now.Date && now.TimeOfDay >= new TimeSpan(2, 30, 0))
                    {
                        Debug.WriteLine($"Saved date {savedDate:yyyy-MM-dd} is older than today {now:yyyy-MM-dd}. Resetting.");
                        obj.DateStart = today;
                        obj.Count = 0;
                    }
                    else
                    {
                        Debug.WriteLine($"No reset needed. SavedDate: {savedDate:yyyy-MM-dd}, Now: {now:yyyy-MM-dd HH:mm}");
                    }
                }

                string updatedJson = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });

                using (var writer = new StreamWriter(filepath, false))
                {
                    await writer.WriteAsync(updatedJson);
                }

                Debug.WriteLine("File updated successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }


    }
}
