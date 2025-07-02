using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Controllers;
using PMACS_V2.Utilities;
using ProgramPartListWeb.Helper;
using System;
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

        public FanMajorController(IMachine man) {
            _man = man;
        }

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
                                ? Convert.ToBase64String(m.Filepath)
                                : string.Empty
                }).ToList();

                if (machineWithImages == null || !machineWithImages.Any())
                {
                    return JsonNotFound("No Fan Major Machine data found");
                }
                return JsonSuccess(machineWithImages);
           
        }
        // GET: P1SA/GetEquipmenList/ID
        public async Task<ActionResult> GetEquipmenList(int sectionID)
        {
            try
            {
                var data = await _man.GetEquipmentData(sectionID);
                if (data == null || !data.Any())
                    return JsonNotFound("No Equipment data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        // GET: P1SA/GetEquipmenList/ID
        public async Task<ActionResult> GetMachineCount(int sectionID, string machcode)
        {
            try
            {
                var data = await _man.GetCountMachine(sectionID, machcode);
                if (data == null || !data.Any())
                    return JsonNotFound("No Equipment data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }

        // ===========================================================
        // ==================== POST FUNCTION ========================
        // ===========================================================
        [HttpPost]
        public async Task<ActionResult> AddNewmachineList()
        {
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
            return Json(obj, JsonRequestBehavior.AllowGet);
            //try
            //{
            //    // Check if there are any files in the request
            //    byte[] getCurrentImage = new byte[] { };

            //    if (Request.Files.Count > 0)
            //    {
            //        var postedFile = Request.Files[0];
            //        string ext = Path.GetExtension(postedFile.FileName);
            //        string filename = "Mach" + DateTime.Now.ToString("yyyyMMddhhmmssffff") + ext;
            //        string imagefile = filename;

            //        // Save the file
            //        string filepathname = "";
            //        string filePath = Server.MapPath("~/Content/Images/" + filename);
            //        // postedFile.SaveAs(filePath);
            //        filepathname = filePath;

            //        getCurrentImage = GlobalUtilities.ResizeAndConvertToBinary(postedFile, filePath);

            //        // DELETE THE IMAGE AFTER IT SAVES TO THE DATABASE
            //        if (System.IO.File.Exists(filepathname))
            //        {
            //            System.IO.File.Delete(filepathname);
            //        }
            //    }


            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}

            ////CacheHelper.Remove("Machine");
            ////bool resut = await _man.AddMachine(obj);

            //return Json("", JsonRequestBehavior.AllowGet);
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
    }
}
