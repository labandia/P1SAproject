using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Controllers;
using PMACS_V2.Utilities;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMACS_V2.Areas.P1SA.Controllers
{
    [CompressResponse]
    public class FanMajorController : ExtendController
    {

        private readonly IMachine _man;

        public FanMajorController(IMachine man) => _man = man;

        // ===========================================================
        // ==================== GET FUNCTIONS ========================
        // ===========================================================

        // GET: P1SA/GetFanmajorMachine
        public async Task<ActionResult> GetFanmajorMachine(int offset, int limit, int sectionID, string mach)
        {
             var data = await _man.GetMachineData(offset, limit, sectionID, mach);
                var machineWithImages = data.Select(m => new
                {
                    m.ID,
                    m.machcode,
                    m.Machname,
                    m.Model,
                    m.Manufact,
                    m.Serial,
                    m.location,
                    m.status,
                    m.DateAdd,
                    m.Asset,
                    m.Equipment,
                    m.Reasons,
                    m.Date_acquired,
                    m.Tongs,
                    m.IsDelete,
                    m.Section_ID,
                    ImageBase64 = (m.Filepath != null && m.Filepath.Length > 0)
                                ? SafeBase64(m.Filepath)
                                : null
                }).ToList();

                if (machineWithImages == null || !machineWithImages.Any())
                {
                    return JsonNotFound("No Fan Major Machine data found");
                }
            //return JsonSuccess(machineWithImages);
                return new JsonResult
                {
                    Data = new { Success = true, Data = machineWithImages },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };

        }

        // GET: P1SA/GetEquipmenList/ID
        public async Task<ActionResult> GetEquipmenList(int sectionID)
        {
            var data = await _man.GetEquipmentData(sectionID) ?? new List<EquipmentList>();
            if (data == null || !data.Any())
                return JsonNotFound("No Equipment data found");

            return JsonSuccess(data);
        }
        // GET: P1SA/GetEquipmenList/ID
        public async Task<ActionResult> GetMachineCount(int sectionID, string machcode)
        {
            var data = await _man.GetCountMachine(sectionID, machcode) ?? new List<CountMachineModel>();
            if (data == null || !data.Any())
                return JsonNotFound("No Equipment data found");

            return JsonSuccess(data);
        }

        // ===========================================================
        // ==================== POST FUNCTION ========================
        // ===========================================================
        [HttpPost]
        public async Task<ActionResult> AddNewmachineList()
        {
            
            //return Json(obj, JsonRequestBehavior.AllowGet);
            try
            {
                // Check if there are any files in the request
                byte[] getCurrentImage = new byte[] { };

                if (Request.Files.Count > 0)
                {
                    var postedFile = Request.Files[0];
                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "Mach" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ext;
                    string imagefile = filename;

                    // Save the file
                    string filepathname = "";
                    string filePath = Server.MapPath("~/Content/Images/" + filename);
                    // postedFile.SaveAs(filePath);
                    filepathname = filePath;

                    getCurrentImage = GlobalUtilities.ResizeAndConvertToBinary(postedFile, filePath);

                    // DELETE THE IMAGE AFTER IT SAVES TO THE DATABASE
                    if (System.IO.File.Exists(filepathname)) System.IO.File.Delete(filepathname);
                    
                }

                var obj = new PostMachineModel
                {
                    MACH_CODE = Request.Form["addmachcode"],
                    Equipment =  Request.Form["Equip"],
                    Date_acquired = Request.Form["Dateacq"],
                    Model = Request.Form["Model"],
                    location = Request.Form["local"],
                    Serial = Request.Form["Serial"],
                    Manufact = Request.Form["Manu"],
                    Asset = Request.Form["Assets"],
                    status = Request.Form["Status"],
                    Shifts = Request.Form["Shifts"],
                    Reasons = Request.Form["addreason"],
                    Filepath = getCurrentImage,
                    Tongs = Request.Form["tons"],
                    Section_ID = Convert.ToInt32(Request.Form["SectionID"])
                };


                bool result = await _man.AddMachine(obj);

                if (result) CacheHelper.Remove("Machinelist");

                return new JsonResult
                {
                    Data = new { Success = true, Data = result },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };

               

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new JsonResult
            {
                Data = new { Success = false, Data = "No Data" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        [HttpPost]
        public async Task<ActionResult> EditmachineList()
        {

            try
            {
                // Check if there are any files in the request
                byte[] getCurrentImage = new byte[] { };

                if (Request.Files.Count > 0)
                {
                    var postedFile = Request.Files[0];
                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "Mach" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ext;
                    string imagefile = filename;

                    // Save the file
                    string filepathname = "";
                    string filePath = Server.MapPath("~/Content/Images/" + filename);
                    // postedFile.SaveAs(filePath);
                    filepathname = filePath;

                    getCurrentImage = GlobalUtilities.ResizeAndConvertToBinary(postedFile, filePath);

                    // DELETE THE IMAGE AFTER IT SAVES TO THE DATABASE
                    if (System.IO.File.Exists(filepathname)) System.IO.File.Delete(filepathname);

                }

                int machineID = Convert.ToInt32(Request.Form["EditID"]);

                var obj = new EditMachineModel
                {
                    ID = machineID,
                    Machname =  Request.Form["EditEquip"],
                    Date_acquired = Request.Form["EditDateacq"],
                    Model = Request.Form["EditModel"],
                    Location = Request.Form["Editlocal"],
                    Serial = Request.Form["EditSerial"],
                    Manufact = Request.Form["EditManu"],
                    Asset = Request.Form["EditAssets"],
                    Status = Request.Form["Editstatus"],
                    Reasons = Request.Form["reason"],
                    Tongs = Request.Form["edittons"],
                    Filepath = getCurrentImage,
                };

                bool result = await _man.EditMachine(obj);

                if (result) CacheHelper.Remove("Machinelist");

                return new JsonResult
                {
                    Data = new { Success = true, Data = await _man.GetMachineDataByID(machineID) },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            return new JsonResult
            {
                Data = new { Success = false, Data = "No Data" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }
        [HttpPost]
        public async Task<ActionResult> DeletemachineList(int machID)
        {

            try
            {
                // Check if there are any files in the request
                byte[] getCurrentImage = new byte[] { };

                if (Request.Files.Count > 0)
                {
                    var postedFile = Request.Files[0];
                    string ext = Path.GetExtension(postedFile.FileName);
                    string filename = "Mach" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ext;
                    string imagefile = filename;

                    // Save the file
                    string filepathname = "";
                    string filePath = Server.MapPath("~/Content/Images/" + filename);
                    // postedFile.SaveAs(filePath);
                    filepathname = filePath;

                    getCurrentImage = GlobalUtilities.ResizeAndConvertToBinary(postedFile, filePath);

                    // DELETE THE IMAGE AFTER IT SAVES TO THE DATABASE
                    if (System.IO.File.Exists(filepathname))
                    {
                        System.IO.File.Delete(filepathname);
                    }
                }

                var obj = new PostMachineModel
                {
                    MACH_CODE = Request.Form["addmachcode"],
                    Equipment =  Request.Form["Equip"],
                    Date_acquired = Request.Form["Dateacq"],
                    Model = Request.Form["Model"],
                    location = Request.Form["local"],
                    Serial = Request.Form["Serial"],
                    Manufact = Request.Form["Manu"],
                    Asset = Request.Form["Assets"],
                    status = Request.Form["Status"],
                    Reasons = Request.Form["addreason"],
                    Tongs = Request.Form["tons"],
                    Section_ID = Convert.ToInt32(Request.Form["SectionID"])
                };
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }


            //bool resut = await _man.AddMachine(obj);

            return Json("", JsonRequestBehavior.AllowGet);
        }


        private string SafeBase64(byte[] bytes)
        {
            try
            {
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                return null;
            }
        }

    }
}
