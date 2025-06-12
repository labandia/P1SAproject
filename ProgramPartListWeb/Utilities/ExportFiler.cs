using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using OfficeOpenXml;
using ProgramPartListWeb.Areas.PC.Models;
using ProgramPartListWeb.Helper;
using Spire.Xls;


namespace ProgramPartListWeb.Utilities
{
    public sealed class ExportFiler
    {
        public static string ExportExcelTemplate(string templatePath, string exportFolder, string newFilename)
        {
            // Ensure EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string exportPath = Path.Combine(exportFolder, newFilename);

            // Make sure the export folder exists
            if (!Directory.Exists(exportFolder))
                Directory.CreateDirectory(exportFolder);

            // Copy the template to the new location with a new name
            File.Copy(templatePath, exportPath, overwrite: true);

            return exportPath;
        }




        public static void SaveFileasPDF(RegistrationModel reg, string json, string department, string outputfilename, string template)
        {
            Task.Run(() =>
            {
                try
                {
                    //string templatePath = HttpContext.Current.Server.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");
                    string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\";
                    string outputPdfPath = Path.Combine(exportFolder, Path.ChangeExtension(outputfilename, ".pdf"));

                    byte[] excelBytes;
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    // Step 1: Generate Excel in memory
                    using (var package = new ExcelPackage(new FileInfo(template)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        worksheet.Cells["B2"].Value = "Registration No: " + reg.RegNo;
                        worksheet.Cells["B4"].Value = "Dept. /Section Inspected: P1SA-" + department;
                        worksheet.Cells["C48"].Value = reg.Manager_Comments;
                        worksheet.Cells["C53"].Value = "Manager: " + reg.Manager;
                        worksheet.Cells["D48"].Value = "Date Conducted: " + reg.DateConduct;
                        worksheet.Cells["D50"].Value = reg.PIC_Comments;

                        var findings = JsonConvert.DeserializeObject<List<FindingModel>>(json);
                        foreach (var f in findings)
                        {
                            switch (f.FindID)
                            {
                                case 1:
                                    worksheet.Cells["B7"].Value = f.FindDescription;
                                    worksheet.Cells["D6"].Value = f.Countermeasure;
                                    break;
                                case 2:
                                    worksheet.Cells["B13"].Value = f.FindDescription;
                                    worksheet.Cells["D12"].Value = f.Countermeasure;
                                    break;
                                case 3:
                                    worksheet.Cells["B20"].Value = f.FindDescription;
                                    worksheet.Cells["D19"].Value = f.Countermeasure;
                                    break;
                                case 4:
                                    worksheet.Cells["B27"].Value = f.FindDescription;
                                    worksheet.Cells["D26"].Value = f.Countermeasure;
                                    break;
                                case 5:
                                    worksheet.Cells["B34"].Value = f.FindDescription;
                                    worksheet.Cells["D33"].Value = f.Countermeasure;
                                    break;
                                default:
                                    worksheet.Cells["B42"].Value = f.FindDescription;
                                    worksheet.Cells["D41"].Value = f.Countermeasure;
                                    break;
                            }
                        }

                        // Save to memory
                        excelBytes = package.GetAsByteArray();
                    }

                    // Step 2: Convert to PDF in memory using Spire.XLS
                    using (var excelStream = new MemoryStream(excelBytes))
                    {
                        var workbook = new Spire.Xls.Workbook();
                        workbook.LoadFromStream(excelStream, ExcelVersion.Version2013);
                        workbook.SaveToFile(outputPdfPath, Spire.Xls.FileFormat.PDF);
                    }

                    // Optional: Notify or log result
                    Debug.WriteLine($"PDF generated at: {outputPdfPath}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error generating PDF: " + ex);
                }
            });
        }
    }
}