using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using Spire.Xls.Core;


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
        public static async Task SaveFileasPDF(RegistrationModel reg, string json, string department, string outputfilename, string template, bool Sign)
        {
            try
            {
                string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Registration\";
                string outputPdfPath = Path.Combine(exportFolder, Path.ChangeExtension(outputfilename, ".pdf"));
                string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

                byte[] excelBytes;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                DateTime date = DateTime.ParseExact(reg.DateConduct, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string formatted = date.ToString("MMMM dd, yyyy");

                // Step 1: Generate Excel in memory
                using (var package = new ExcelPackage(new FileInfo(template)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    worksheet.Cells["B2"].Value = "Registration No: " + reg.RegNo;
                    worksheet.Cells["B4"].Value = "Dept. /Section Inspected: P1SA-" + department;
                    worksheet.Cells["C48"].Value = reg.Manager_Comments;
                    worksheet.Cells["C53"].Value = "Manager: " + reg.Manager;
                    worksheet.Cells["D48"].Value = " Date Conducted: " + formatted;
                    worksheet.Cells["D49"].Value = " Comment (Person In-Charge):  " + reg.PIC;
                    worksheet.Cells["G53"].Value = reg.PIC;
                    worksheet.Cells["D50"].Value = reg.PIC_Comments;
                    worksheet.Cells["E53"].Value = reg.FullName;

                    var findings = JsonConvert.DeserializeObject<List<FindingModel>>(json);
                    foreach (var f in findings)
                    {
                        switch (f.FindID)
                        {
                            case 1: worksheet.Cells["B7"].Value = f.FindDescription; worksheet.Cells["D6"].Value = f.Countermeasure; break;
                            case 2: worksheet.Cells["B13"].Value = f.FindDescription; worksheet.Cells["D12"].Value = f.Countermeasure; break;
                            case 3: worksheet.Cells["B20"].Value = f.FindDescription; worksheet.Cells["D19"].Value = f.Countermeasure; break;
                            case 4: worksheet.Cells["B27"].Value = f.FindDescription; worksheet.Cells["D26"].Value = f.Countermeasure; break;
                            case 5: worksheet.Cells["B34"].Value = f.FindDescription; worksheet.Cells["D33"].Value = f.Countermeasure; break;
                            default: worksheet.Cells["B42"].Value = f.FindDescription; worksheet.Cells["D41"].Value = f.Countermeasure; break;
                        }
                    }

                    excelBytes = package.GetAsByteArray();
                }

                // Step 2: Convert to PDF using Spire.XLS
                using (var excelStream = new MemoryStream(excelBytes))
                {
                    var workbook = new Spire.Xls.Workbook();
                    workbook.LoadFromStream(excelStream, ExcelVersion.Version2013);
                    var sheet = workbook.Worksheets[0];

                    var affectedCells = new[] { "B42", "D41", "C41", "D42" };
                    foreach (var cell in affectedCells)
                    {
                        var range = sheet.Range[cell];
                        range.Style.VerticalAlignment = VerticalAlignType.Center;
                        range.Style.WrapText = true;
                    }

                    sheet.SetRowHeight(41, 30);
                    sheet.SetRowHeight(42, 30);

                    // ⭐️ Conditional image insertion
                    if (Sign)
                    {
                        string imagePath = await GetImageString(reg.Employee_ID);
                        if (!string.IsNullOrEmpty(imagePath))
                        {
                            string pathfile = Path.Combine(@"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures", imagePath);
                            if (File.Exists(pathfile))
                            {
                                foreach (IShape shape in sheet.PrstGeomShapes)
                                {
                                    if (shape.Name == "SignaImage" && shape is IPrstGeomShape imageShape)
                                    {
                                        imageShape.Fill.CustomPicture(pathfile);
                                        Debug.WriteLine("✔ Signature image added to shape.");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Debug.WriteLine("⚠ Image file not found: " + pathfile);
                            }
                        }
                    }

                    workbook.SaveToFile(tempPdfPath, FileFormat.PDF);
                }

                File.Copy(tempPdfPath, outputPdfPath, overwrite: true);
                File.Delete(tempPdfPath);

                Debug.WriteLine($"✔ PDF successfully generated at: {outputPdfPath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("❌ Error generating PDF: " + ex.Message);
            }
        }


        public static void SaveFileasExcel(HttpPostedFileBase ExcelFile, string FilenameExtension)
        {
            string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Countermeasure\";
            string saveFilePath = $@"{exportFolder}{FilenameExtension}";
            ExcelFile.SaveAs(saveFilePath);
        }


        //public static async Task SaveFileasPDF(RegistrationModel reg, string json, string department, string outputfilename, string template, bool Sign)
        //{
        //    try
        //    {
        //        //string templatePath = HttpContext.Current.Server.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");
        //        string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\";
        //        string outputPdfPath = Path.Combine(exportFolder, Path.ChangeExtension(outputfilename, ".pdf"));
        //        string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

        //        byte[] excelBytes;
        //        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //        DateTime date = DateTime.ParseExact(reg.DateConduct, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //        string formatted = date.ToString("MMMM dd, yyyy");  // Output: "July 24, 2025"

        //        // Step 1: Generate Excel in memory
        //        using (var package = new ExcelPackage(new FileInfo(template)))
        //        {
        //            var worksheet = package.Workbook.Worksheets[0];

        //            worksheet.Cells["B2"].Value = "Registration No: " + reg.RegNo;
        //            worksheet.Cells["B4"].Value = "Dept. /Section Inspected: P1SA-" + department;
        //            worksheet.Cells["C48"].Value = reg.Manager_Comments;
        //            worksheet.Cells["C53"].Value = "Manager: " + reg.Manager;
        //            worksheet.Cells["D48"].Value = " Date Conducted: " + formatted;
        //            worksheet.Cells["D49"].Value = " Comment (Person In-Charge):  " + reg.PIC;
        //            worksheet.Cells["G53"].Value = reg.PIC;
        //            worksheet.Cells["D50"].Value = reg.PIC_Comments;
        //            worksheet.Cells["E53"].Value = reg.FullName;


        //            var findings = JsonConvert.DeserializeObject<List<FindingModel>>(json);
        //            foreach (var f in findings)
        //            {
        //                switch (f.FindID)
        //                {
        //                    case 1:
        //                        worksheet.Cells["B7"].Value = f.FindDescription;
        //                        worksheet.Cells["D6"].Value = f.Countermeasure;
        //                        break;
        //                    case 2:
        //                        worksheet.Cells["B13"].Value = f.FindDescription;
        //                        worksheet.Cells["D12"].Value = f.Countermeasure;
        //                        break;
        //                    case 3:
        //                        worksheet.Cells["B20"].Value = f.FindDescription;
        //                        worksheet.Cells["D19"].Value = f.Countermeasure;
        //                        break;
        //                    case 4:
        //                        worksheet.Cells["B27"].Value = f.FindDescription;
        //                        worksheet.Cells["D26"].Value = f.Countermeasure;
        //                        break;
        //                    case 5:
        //                        worksheet.Cells["B34"].Value = f.FindDescription;
        //                        worksheet.Cells["D33"].Value = f.Countermeasure;
        //                        break;
        //                    default:
        //                        worksheet.Cells["B42"].Value = f.FindDescription;
        //                        worksheet.Cells["D41"].Value = f.Countermeasure;
        //                        break;
        //                }
        //            }

        //            // Save to memory
        //            excelBytes = package.GetAsByteArray();
        //        }

        //        // Step 2: Convert to PDF in memory using Spire.XLS
        //        using (var excelStream = new MemoryStream(excelBytes))
        //        {
        //            var workbook = new Spire.Xls.Workbook();
        //            workbook.LoadFromStream(excelStream, ExcelVersion.Version2013);

        //            var sheet = workbook.Worksheets[0];

        //            // ✨ Fix: Set top alignment and wrap to avoid vertical space
        //            var affectedCells = new[] { "B42", "D41", "C41", "D42" };
        //            foreach (var cell in affectedCells)
        //            {
        //                var range = sheet.Range[cell];
        //                range.Style.VerticalAlignment = VerticalAlignType.Top;
        //                range.Style.WrapText = true;

        //            }

        //            // ✨ Optional: Adjust row heights to avoid excess space
        //            sheet.SetRowHeight(41, 30);
        //            sheet.SetRowHeight(42, 30);

        //            // ⭐️ Next Step : Insert user signature image at C55
        //            string imagePath = await GetImageString(reg.Employee_ID);
        //            if (!string.IsNullOrEmpty(imagePath))
        //            {
        //                string pathfile = Path.Combine(@"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures", imagePath);
        //                Debug.WriteLine("HERE : " +  pathfile);
        //                //CellRange cell = sheet.Range["E53"];
        //                //ExcelPicture picture = sheet.Pictures.Add(cell.Row, cell.Column, pathfile);

        //                //picture.TopRowOffset = 0;
        //                //picture.LeftColumnOffset = 0;
        //                //picture.Width = (int)(cell.ColumnWidth * 7); // Scale approx. to column width
        //                //picture.Height = (int)sheet.Rows[cell.Row - 1].RowHeight; // Match row height

        //                if (File.Exists(pathfile))
        //                {
        //                    foreach (IShape shape in sheet.PrstGeomShapes)
        //                    {
        //                        if (shape.Name == "SignaImage")
        //                        {
        //                            // Make sure it's a shape that supports image fill
        //                            if (shape is IPrstGeomShape imageShape)
        //                            {
        //                                // Apply the image fill
        //                                imageShape.Fill.CustomPicture(pathfile);

        //                                // Optional: Auto-size the shape to image dimensions or vice versa
        //                                // This is optional; you can also resize the shape manually
        //                                //System.Drawing.Image img = System.Drawing.Image.FromFile(pathfile);
        //                                //shape.Width = img.Width;
        //                                //shape.Height = img.Height;
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    Debug.WriteLine("Image file not found at: " + pathfile);
        //                }
        //            }

        //            // Save to temporary local file first
        //            workbook.SaveToFile(tempPdfPath, FileFormat.PDF);
        //        }

        //        File.Copy(tempPdfPath, outputPdfPath, overwrite: true);
        //        File.Delete(tempPdfPath);

        //        // Optional: Notify or log result
        //        Debug.WriteLine($"PDF successfully generated at: {outputPdfPath}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Error generating PDF: " + ex);
        //    }
        //}

        private static async Task<string>GetImageString(string EmpID)
        {
            string strquery = $@"SELECT u.Signature
                                FROM UserAccounts ua
                                INNER JOIN Users u ON u.User_ID = ua.User_ID
                                INNER JOIN ProjectList p ON p.Project_ID = ua.Project_ID
                                WHERE IsActive = 1 AND u.Employee_ID =@ID";

            string data = await SqlDataAccess.GetOneData(strquery, new { ID = EmpID });
            return (!string.IsNullOrEmpty(data)) ? data : "";   
        }
    }
}