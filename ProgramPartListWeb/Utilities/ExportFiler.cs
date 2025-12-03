using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
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
        private static byte[] _cachedTemplateBytes;
        private static readonly object templateLock = new object();

        private static byte[] LoadTemplate(string template)
        {
            if (_cachedTemplateBytes != null)
                return _cachedTemplateBytes;

            lock (templateLock)
            {
                if (_cachedTemplateBytes == null)
                    _cachedTemplateBytes = File.ReadAllBytes(template);
            }

            return _cachedTemplateBytes;
        }


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


        public static async Task SaveFileasPDFV2(
               AddFormRegistrationModel reg,
               string json,
               string fullname, // -- name of the PIC
               string inspectname, // -- name of the Inspector  
               string department,
               string outputfilename,
               string template,
               string IsSignature,
               bool Sign)
        {
            try
            {
                string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Registration\";
                string finalPdf = Path.Combine(exportFolder, outputfilename);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Load Excel template
                byte[] templateBytes = LoadTemplate(template);

                // Deserialize JSON
                var findings = JsonConvert.DeserializeObject<List<FindingModel>>(json);
                if (findings == null) findings = new List<FindingModel>();

                await Task.Run(async () =>
                {
                    byte[] excelBytes;

                    // ----------------------------------------------------
                    // STEP 1 — BUILD EXCEL
                    // ----------------------------------------------------
                    using (var templateStream = new MemoryStream(templateBytes))
                    using (var package = new ExcelPackage(templateStream))
                    {
                        var ws = package.Workbook.Worksheets[0];

                        ws.Cells["B2"].Value = "Registration No: " + reg.RegNo;
                        ws.Cells["B4"].Value = "Dept. /Section Inspected: P1SA-" + department;
                        ws.Cells["G53"].Value = fullname;
                        ws.Cells["E53"].Value = inspectname;

                        foreach (var f in findings)
                        {
                            string targetCell = "B42"; // default

                            switch (f.FindID)
                            {
                                case 1: targetCell = "B7"; break;
                                case 2: targetCell = "B13"; break;
                                case 3: targetCell = "B20"; break;
                                case 4: targetCell = "B27"; break;
                                case 5: targetCell = "B34"; break;
                                default: targetCell = "B42"; break;
                            }

                            ws.Cells[targetCell].Value = f.FindDescription;
                        }

                        excelBytes = package.GetAsByteArray();
                    }
                    // ----------------------------------------------------
                    // STEP 2 — BUILD PDF
                    // ----------------------------------------------------
                    using (var excelStream = new MemoryStream(excelBytes))
                    {
                        var workbook = new Spire.Xls.Workbook();
                        workbook.LoadFromStream(excelStream, ExcelVersion.Version2013);

                        var sheet = workbook.Worksheets[0];

                        // Row height + wrapping
                        sheet.Range["B42"].Style.WrapText = true;
                        sheet.SetRowHeight(41, 30);
                        sheet.SetRowHeight(42, 30);

                        // ----------------------------------------------------
                        // STEP 3 — OPTIONAL SIGNATURE INSERTION
                        // ----------------------------------------------------
                        if (Sign)
                        {
                            string imageName = IsSignature;
                            Debug.WriteLine("Filname: " + imageName);
                            if (!string.IsNullOrEmpty(imageName))
                            {
                                string imagePath = Path.Combine(
                                    @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures",
                                    imageName
                                );

                                if (File.Exists(imagePath))
                                {
                                    foreach (IShape shape in sheet.PrstGeomShapes)
                                    {
                                        if (shape.Name == "SignaImage" && shape is IPrstGeomShape imgShape)
                                        {
                                            imgShape.Fill.CustomPicture(imagePath);
                                            Debug.WriteLine("✔ Signature added.");
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    Debug.WriteLine("⚠ Signature file missing: " + imagePath);
                                }
                            }
                        }
                        //string strquery = $@"SELECT TOP 1 Signature
                        //         FROM Patrol_UserEmail WHERE Employee_ID =@ID";

                        //string data = await SqlDataAccess.GetOneData(strquery, new { ID = reg.Employee_ID });
                        //string imageName = (!string.IsNullOrEmpty(data)) ? data : "";
                        
                        //Debug.WriteLine("Image file : " + imageName);

                        // Save PDF
                        workbook.SaveToFile(finalPdf, FileFormat.PDF);
                    }
                });

                Debug.WriteLine("PDF saved: " + finalPdf);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("❌ PDF ERROR: " + ex);
            }
        }





        public static async Task UpdatePDFRegistration(
                    PatrolRegistrationViewModel reg,
                    List<FindingModel> find,
                    List<EmailModelV2> processowner,
                    List<EmailSignatures> emails,
                    string previousFile,
                    string outputfilename,
                    string template)
        {
            try
            {
                // ✅ Step 1: Define paths
                string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Registration\";
                string outputPdfPath = Path.Combine(exportFolder, Path.ChangeExtension(outputfilename, ".pdf"));
                string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

                // ✅ Step 2: Basic validations
                if (reg == null)
                    throw new ArgumentNullException(nameof(reg), "Registration data cannot be null.");

                if (!File.Exists(template))
                    throw new FileNotFoundException("Template file not found.", template);


                // ✅ Step 3: Resolve names safely
                string getFullname = processowner.SingleOrDefault(x => x.Employee_ID == reg.PIC_ID)?.FullName ?? "N/A";
                string getFullnameInspect = processowner.SingleOrDefault(x => x.Employee_ID == reg.Inspect_ID)?.FullName ?? "N/A";
                string getManagersName = processowner.SingleOrDefault(x => x.Employee_ID == reg.Manager_ID)?.FullName ?? "N/A";

                // ✅ Step 4: Format date safely
                



                // 🧹 Step 0: Delete old file if exists
                if (!string.IsNullOrEmpty(previousFile))
                {
                    string oldFilePath = Path.Combine(exportFolder, previousFile);

                    if (File.Exists(oldFilePath))
                    {
                        try
                        {
                            File.Delete(oldFilePath);
                            Debug.WriteLine($"🗑️ Deleted old file: {oldFilePath}");
                        }
                        catch (Exception delEx)
                        {
                            Debug.WriteLine($"⚠️ Could not delete old file: {delEx.Message}");
                        }
                    }
                    else
                    {
                        Debug.WriteLine("ℹ️ No old file found to delete.");
                    }
                }


                // ✅ Step 6: Prepare Excel using EPPlus
                byte[] excelBytes = null;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


                await Task.Run(() =>
                {
                    using (var package = new ExcelPackage(new FileInfo(template)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];

                        worksheet.Cells["B2"].Value = "Registration No: " + reg.RegNo;
                        worksheet.Cells["B4"].Value = "Dept. /Section Inspected: P1SA-" + reg.SectionName;
                        worksheet.Cells["C53"].Value = "Manager: " + getManagersName;
                        worksheet.Cells["C48"].Value = reg.Manager_Comments;
                        worksheet.Cells["D48"].Value = "Date Conducted: " + (reg.DateConduct ?? "N/A");
                        worksheet.Cells["D50"].Value = reg.PIC_Comments;
                        worksheet.Cells["G53"].Value = getFullname;
                        worksheet.Cells["E53"].Value = getFullnameInspect;

                        // Fill findings safely
                        if (find != null)
                        {
                            foreach (var f in find)
                            {
                                string findCell;
                                string counterCell;

                                switch (f.FindID)
                                {
                                    case 1:
                                        findCell = "B7"; counterCell = "D6";
                                        break;
                                    case 2:
                                        findCell = "B13"; counterCell = "D12";
                                        break;
                                    case 3:
                                        findCell = "B20"; counterCell = "D19";
                                        break;
                                    case 4:
                                        findCell = "B27"; counterCell = "D26";
                                        break;
                                    case 5:
                                        findCell = "B34"; counterCell = "D33";
                                        break;
                                    default:
                                        findCell = "B42"; counterCell = "D41";
                                        break;
                                }

                                worksheet.Cells[findCell].Value = f.FindDescription;
                                worksheet.Cells[counterCell].Value = f.Countermeasure;
                            }
                        }

                        excelBytes = package.GetAsByteArray();
                    }
                }); // end Task.Run for Excel generation

                // Convert Excel to PDF using Spire.XLS (synchronous -> run on threadpool)
                await Task.Run( () =>
                {
                    using (var excelStream = new MemoryStream(excelBytes))
                    {
                        var workbook = new Spire.Xls.Workbook();
                        workbook.LoadFromStream(excelStream, ExcelVersion.Version2013);
                        var sheet = workbook.Worksheets[0];

                        // Apply some layout changes
                        string[] affectedCells = { "B42", "D41", "C41", "D42" };
                        foreach (var cell in affectedCells)
                        {
                            var range = sheet.Range[cell];
                            if (range != null)
                            {
                                range.Style.VerticalAlignment = VerticalAlignType.Center;
                                range.Style.WrapText = true;
                            }
                        }

                        sheet.SetRowHeight(41, 30);
                        sheet.SetRowHeight(42, 30);


                        // Import Images Signatures
                        // 
                        foreach(var emailSig in emails)
                        {
                            string imageName = emailSig.Signature;
                            string imagePath = Path.Combine(
                               @"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures",
                               imageName
                            );


                            if (File.Exists(imagePath))
                            {
                                foreach (IShape shape in sheet.PrstGeomShapes)
                                {
                                    if(emailSig.Position == 5)
                                    {
                                        if (shape.Name == "SignaImage" && shape is IPrstGeomShape imgShape)
                                        {
                                            imgShape.Fill.CustomPicture(imagePath);
                                            Debug.WriteLine("✔ Signature added.");
                                        }
                                    }

                                    if (emailSig.Position == 4)
                                    {
                                        if (shape.Name == "Supervisor" && shape is IPrstGeomShape imgShape)
                                        {
                                            imgShape.Fill.CustomPicture(imagePath);
                                            Debug.WriteLine("✔ Signature added.");
                                        }
                                    }


                                    if (emailSig.Position == 3)
                                    {
                                        if (shape.Name == "Department" && shape is IPrstGeomShape imgShape)
                                        {
                                            imgShape.Fill.CustomPicture(imagePath);
                                            Debug.WriteLine("✔ Signature added.");
                                        }
                                    }

                                    if (emailSig.Position == 1)
                                    {
                                        if (shape.Name == "Division" && shape is IPrstGeomShape imgShape)
                                        {
                                            imgShape.Fill.CustomPicture(imagePath);
                                            Debug.WriteLine("✔ Signature added.");
                                        }
                                    }

                                }
                            }
                            else
                            {
                                Debug.WriteLine("⚠ Signature file missing: " + imagePath);
                            }
                        }
    
                        

                        workbook.SaveToFile(tempPdfPath, FileFormat.PDF);
                    }
                });

                // Async copy from tempPdfPath to outputPdfPath using streams (no await using)
                // Ensure destination folder exists
                try
                {
                    var destDir = Path.GetDirectoryName(outputPdfPath);
                    if (!Directory.Exists(destDir))
                        Directory.CreateDirectory(destDir);
                }
                catch (Exception dirEx)
                {
                    Debug.WriteLine($"⚠️ Could not create destination directory: {dirEx.Message}");
                    // proceed — File.Create will throw if directory missing
                }

                using (var sourceStream = new FileStream(tempPdfPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var destinationStream = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                }

                // Delete temp file (run in background thread)
                try
                {
                    await Task.Run(() => File.Delete(tempPdfPath));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"⚠️ Could not delete temp file: {ex.Message}");
                }

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

            // Check if file already exists, then delete it
            if (System.IO.File.Exists(saveFilePath))
            {
                System.IO.File.Delete(saveFilePath);
            }

            // Save the File
            ExcelFile.SaveAs(saveFilePath);
        }


        public static async Task SaveFileasPDFV2(RegistrationModel reg, string json, string department, string outputfilename, string template, bool Sign)
        {
            try
            {
                //string templatePath = HttpContext.Current.Server.MapPath("~/Content/Uploads/PGFY-00031FORM_1.xlsx");
                string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\";
                string outputPdfPath = Path.Combine(exportFolder, Path.ChangeExtension(outputfilename, ".pdf"));
                string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

                byte[] excelBytes;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                DateTime date = DateTime.ParseExact(reg.DateConduct, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                string formatted = date.ToString("MMMM dd, yyyy");  // Output: "July 24, 2025"

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

                    var sheet = workbook.Worksheets[0];

                    // ✨ Fix: Set top alignment and wrap to avoid vertical space
                    var affectedCells = new[] { "B42", "D41", "C41", "D42" };
                    foreach (var cell in affectedCells)
                    {
                        var range = sheet.Range[cell];
                        range.Style.VerticalAlignment = VerticalAlignType.Top;
                        range.Style.WrapText = true;

                    }

                    // ✨ Optional: Adjust row heights to avoid excess space
                    sheet.SetRowHeight(41, 30);
                    sheet.SetRowHeight(42, 30);

                    // ⭐️ Next Step : Insert user signature image at C55
                    string imagePath = await GetImageString(reg.Employee_ID);
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        string pathfile = Path.Combine(@"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures", imagePath);
                        Debug.WriteLine("HERE : " + pathfile);
                        //CellRange cell = sheet.Range["E53"];
                        //ExcelPicture picture = sheet.Pictures.Add(cell.Row, cell.Column, pathfile);

                        //picture.TopRowOffset = 0;
                        //picture.LeftColumnOffset = 0;
                        //picture.Width = (int)(cell.ColumnWidth * 7); // Scale approx. to column width
                        //picture.Height = (int)sheet.Rows[cell.Row - 1].RowHeight; // Match row height

                        if (File.Exists(pathfile))
                        {
                            foreach (IShape shape in sheet.PrstGeomShapes)
                            {
                                if (shape.Name == "SignaImage")
                                {
                                    // Make sure it's a shape that supports image fill
                                    if (shape is IPrstGeomShape imageShape)
                                    {
                                        // Apply the image fill
                                        imageShape.Fill.CustomPicture(pathfile);

                                        // Optional: Auto-size the shape to image dimensions or vice versa
                                        // This is optional; you can also resize the shape manually
                                        //System.Drawing.Image img = System.Drawing.Image.FromFile(pathfile);
                                        //shape.Width = img.Width;
                                        //shape.Height = img.Height;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Image file not found at: " + pathfile);
                        }
                    }

                    // Save to temporary local file first
                    workbook.SaveToFile(tempPdfPath, FileFormat.PDF);
                }

                File.Copy(tempPdfPath, outputPdfPath, overwrite: true);
                File.Delete(tempPdfPath);

                // Optional: Notify or log result
                Debug.WriteLine($"PDF successfully generated at: {outputPdfPath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error generating PDF: " + ex);
            }
        }

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


        public static async Task<string> GetImageStringP1SA(string EmpID)
        {
            string strquery = $@"SELECT TOP 1 Signature
                                 FROM Patrol_UserEmail WHERE Employee_ID =@ID";

            string data = await SqlDataAccess.GetOneData(strquery, new { ID = EmpID });
            return (!string.IsNullOrEmpty(data)) ? data : "";
        }






        public static async Task UpdatePDFRegistrationV2(
    PatrolRegistrationViewModel reg,
    List<FindingModel> find,
    List<EmailModelV2> processowner,
    string previousFile,
    string outputfilename,
    string template)
        {
            try
            {
                string exportFolder = @"\\SDP010F6C\Users\USER\Pictures\Access\Excel\Patrol_Registration\";
                string outputPdf = Path.Combine(exportFolder, Path.ChangeExtension(outputfilename, ".pdf"));
                string tempPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");

                // ================================
                // 1. Generate Excel using EPPlus
                // ================================
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                byte[] excelBytes;

                using (var pkg = new ExcelPackage(new FileInfo(template)))
                {
                    var ws = pkg.Workbook.Worksheets[0];

                    // Basic info
                    ws.Cells["B2"].Value = "Registration No: " + reg.RegNo;
                    ws.Cells["B4"].Value = "Dept. / Section Inspected: P1SA-" + reg.SectionName;
                    ws.Cells["C53"].Value = "Manager: " + processowner.FirstOrDefault(p => p.Employee_ID == reg.Manager_ID)?.FullName;
                    ws.Cells["E53"].Value = processowner.FirstOrDefault(p => p.Employee_ID == reg.Inspect_ID)?.FullName;  // Inspector
                    ws.Cells["G53"].Value = processowner.FirstOrDefault(p => p.Employee_ID == reg.PIC_ID)?.FullName;       // PIC / Supervisor

                    ws.Cells["C48"].Value = reg.Manager_Comments;
                    ws.Cells["D48"].Value = "Date Conducted: " + (reg.DateConduct ?? "N/A");
                    ws.Cells["D50"].Value = reg.PIC_Comments;

                    // Fill findings
                    if (find != null)
                    {
                        foreach (var f in find)
                        {
                            switch (f.FindID)
                            {
                                case 1: ws.Cells["B7"].Value = f.FindDescription; ws.Cells["D6"].Value = f.Countermeasure; break;
                                case 2: ws.Cells["B13"].Value = f.FindDescription; ws.Cells["D12"].Value = f.Countermeasure; break;
                                case 3: ws.Cells["B20"].Value = f.FindDescription; ws.Cells["D19"].Value = f.Countermeasure; break;
                                case 4: ws.Cells["B27"].Value = f.FindDescription; ws.Cells["D26"].Value = f.Countermeasure; break;
                                case 5: ws.Cells["B34"].Value = f.FindDescription; ws.Cells["D33"].Value = f.Countermeasure; break;
                                default: ws.Cells["B42"].Value = f.FindDescription; ws.Cells["D41"].Value = f.Countermeasure; break;
                            }
                        }
                    }

                    excelBytes = pkg.GetAsByteArray();
                }

                // =========================================
                // 2. Load Excel in Spire.XLS and insert IMAGES
                // =========================================
                using (MemoryStream ms = new MemoryStream(excelBytes))
                {
                    Workbook wb = new Workbook();
                    wb.LoadFromStream(ms);
                    Worksheet ws = wb.Worksheets[0];

                    // ----------------------------
                    // 🔥 Final: Insert real pictures
                    // ----------------------------
                    await InsertSignature(ws, "E53", reg.Inspect_ID);      // Inspector
                    await InsertSignature(ws, "C53", reg.Manager_ID);      // Manager
                    await InsertSignature(ws, "G53", reg.PIC_ID);          // Supervisor / PIC
                    await InsertSignature(ws, "B53", reg.DivManager_ID);   // Division Manager

                    // Export PDF
                    wb.SaveToFile(tempPdf, FileFormat.PDF);
                }

                // Save final output
                File.Copy(tempPdf, outputPdf, true);
                File.Delete(tempPdf);

                Debug.WriteLine("✔ PDF Generated: " + outputPdf);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("❌ PDF Error: " + ex);
            }
        }

        private static async Task InsertSignature(Worksheet sheet, string cellRef, string empID)
        {
            if (string.IsNullOrEmpty(empID))
                return;

            string fileName = await GetImageStringP1SA(empID);
            if (string.IsNullOrEmpty(fileName))
                return;

            string path = Path.Combine(@"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Signatures", fileName);

            if (!File.Exists(path))
                return;

            CellRange cell = sheet.Range[cellRef];

            // Insert Excel picture (This always appears in PDF!)
            ExcelPicture pic = sheet.Pictures.Add(cell.Row, cell.Column, path);

            // Auto-resize
            pic.Width = 110;
            pic.Height = 45;

            // Align to cell
            pic.TopRowOffset = 2;
            pic.LeftColumnOffset = 2;
        }

    }
}