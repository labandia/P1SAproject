using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Areas.P1SA.Repository;
using PMACS_V2.Controllers;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMACS_V2.Areas.P1SA.Controllers
{
    public class PostCapacityController : ExtendController
    {
        private readonly ICapacity _cap;

        public PostCapacityController(ICapacity cap) => _cap = cap;

        [HttpPost]
        public async Task<ActionResult> P1saSummaryUpdated(PsummaryModel cap)
        {
            bool result = await _cap.EditP1SAsummary(cap);

            if (!result) return JsonValidationError();

            CacheHelper.Remove("p1sasummary");
            await UpdateRepository.UpdateUserLogs(9, 1, "Edit");

            return JsonCreated(cap, "Update successfully");
        }

       
        [HttpPost]
        public async Task<ActionResult> UpdateProcessCap()
        {
            int capid = Convert.ToInt32(Request.Form["Capgroup_ID"]);
            //double Cycletime = Convert.ToDouble(Request.Form["Proc_CycleTime"]);

            var obj = new ProcessformPostModel
            {
                ProcessCode = Request.Form["ProcessCode"],
                Days = Convert.ToDouble(Request.Form["Proc_Days"]),
                Months = Convert.ToInt32(Request.Form["Proc_Months"]),
                OperationTime = Convert.ToDouble(Request.Form["Proc_OperationTime"]),
                Cap_Per_Machine = Convert.ToInt32(Request.Form["Proc_Cap_Per_Machine"])
            };

            var update1 = _cap.UpdateProcessform(obj);
            var update2 = UpdateGroupCapacitySummary(capid, Request.Form["ProcessCode"]);        

            bool[] results = await Task.WhenAll(update1, update2);

            if (!results.All(r => r)) JsonValidationError();

            return JsonCreated(obj, "Update Process");

        }


        [HttpPost]
        public async Task<ActionResult> UpdatedPMACSProcess()
        {
            var obj = new ProcessformPostModel
            {
                ProcessCode = "",
                Days = 0,
                Months = 0,
                OperationTime = 0,
                Cap_Per_Machine = 0,
            };

            bool result = await _cap.UpdateProcessform(obj);
            if (!result) JsonValidationError();
            return JsonCreated(obj, "Updated Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> AddMoldingModelBase(AddMoldingModelPost add)
        {
            int capid = Convert.ToInt32(Request.Form["Capgroup_ID"]);
            var update1 = _cap.AddMoldingModels(add);
            var update2 = UpdateGroupCapacitySummary(capid, Request.Form["ProcessCode"]);

            bool[] results = await Task.WhenAll(update1, update2);
            if (!results.All(r => r)) JsonValidationError();

            CacheHelper.Remove("Molding");
            await UpdateRepository.UpdateUserLogs(5, 1, "Add");
            return JsonCreated(add, "Update Process");
        }
        [HttpPost]
        public async Task<ActionResult> EditMoldingByDetails()
        {
            int capid = Convert.ToInt32(Request.Form["Capgroup_ID"]);

            var mold = new MoldingPostmodel
            {
                Capinfo_ID = Convert.ToInt32(Request.Form["Detail_Capinfo_ID"]),
                CycleTime = Convert.ToDouble(Request.Form["Detail_CycleTime"]),
                Actual_cav =  Convert.ToInt32(Request.Form["Detail_Actual_cav"]),
                DieQty =  Convert.ToInt32(Request.Form["Detail_Dieqty"]),
                Operation_time =  Convert.ToDouble(Request.Form["Detail_Operation_time"]),
                Partnum =  Request.Form["Detail_Partnum"]
            };

            var update1 = _cap.EditMoldingModels(mold);
            var update2 = UpdateGroupCapacitySummary(capid, Request.Form["ProcessCode"]);

            bool[] results = await Task.WhenAll(update1, update2);
            if (!results.All(r => r)) JsonValidationError();

            CacheHelper.Remove("Molding");
            await UpdateRepository.UpdateUserLogs(5, 1, "Edit");
            return JsonCreated(mold, "Update Successfully");
        }
        


        [HttpPost]
        public async Task<ActionResult> EditRotorByDetails()
        {
            int capid = Convert.ToInt32(Request.Form["Capgroup_ID"]);

            var rotor = new RotorModel
            {
                Capinfo_ID = Convert.ToInt32(Request.Form["Detail_Capinfo_ID"]),
                CycleTime = Convert.ToDouble(Request.Form["Detail_CycleTime"]),
                Cover =  Request.Form["Detail_Cover"],
                Dream =  Request.Form["Detail_Dream"],
                Impeller = Request.Form["Detail_Impeller"],
                Operation_time =  Convert.ToDouble(Request.Form["Detail_Operation_time"]),
            };

            var update1 = _cap.EditRotorModels(rotor);
            var update2 = UpdateGroupCapacitySummary(capid, Request.Form["ProcessCode"]);

            bool[] results = await Task.WhenAll(update1, update2);
            if (!results.All(r => r)) JsonValidationError();

            CacheHelper.Remove("Rotor");
            await UpdateRepository.UpdateUserLogs(6, 1, "Edit");
            return JsonCreated(rotor, "Update Successfully");
        }

        [HttpPost]
        public async Task<ActionResult> AddPressModelBase(PressModel press)
        {
            int capid = Convert.ToInt32(Request.Form["Capgroup_ID"]);
            var pressobj = new PressModel();
            pressobj.foredata = Convert.ToInt32(Request.Form["forecast"]);
            pressobj.Operation_time = Convert.ToDouble(Request.Form["Operation_time"]);
            pressobj.Capgroup_ID = capid;

            if (capid == 10)
            {
                pressobj.Lam = Request.Form["Lam"];
                pressobj.SPM = Request.Form["SPM"];
                pressobj.Row = Request.Form["Row"];
            }
            else if (capid == 11)
            {
                pressobj.CycleTime = Convert.ToDouble(Request.Form["CycleTime"]);
            }
            else
            {
                pressobj.TotalCycle = Request.Form["TotalCycle"];
                pressobj.Bucket = Request.Form["Bucket"];
                pressobj.Cycle_cnc = Request.Form["Cycle_cnc"];
                pressobj.Apearance_inspect = Request.Form["Apearance_inspect"];
                pressobj.Laser = Request.Form["Laser"];
                pressobj.Apearance_inspect = Request.Form["Apearance_inspect"];
                pressobj.Pre_wash = Request.Form["Pre_wash"];
                pressobj.Wash_cycle = Request.Form["Wash_cycle"];
            }


            var update1 = _cap.AddPressModels(press);
            var update2 = UpdateGroupCapacitySummary(capid, Request.Form["ProcessCode"]);

            bool[] results = await Task.WhenAll(update1, update2);
            if (!results.All(r => r)) JsonValidationError();

            CacheHelper.Remove("Press");
            await UpdateRepository.UpdateUserLogs(6, 1, "Add");
            return JsonCreated(pressobj, "Update Successfully");
        }
        [HttpPost]
        public async Task<ActionResult> EditPressByDetails()
        {
            int capid = Convert.ToInt32(Request.Form["Capgroup_ID"]);
            var press = new PressModel();
            press.Capinfo_ID = Convert.ToInt32(Request.Form["Detail_Capinfo_ID"]);
            press.Operation_time = Convert.ToDouble(Request.Form["Detail_Operation_time"]);
            press.Capgroup_ID = capid;

            if (capid == 10)
            {    
                press.Lam = Request.Form["Detail_Lam"];
                press.SPM = Request.Form["Detail_SPM"];
                press.Row = Request.Form["Detail_Row"];
            }
            else if(capid == 11)
            {
                press.CycleTime = Convert.ToDouble(Request.Form["Detail_CycleTime"]);
            }
            else
            {
                press.TotalCycle = Request.Form["Detail_TotalCycle"];
                press.Bucket = Request.Form["Detail_Bucket"];
                press.Cycle_cnc = Request.Form["Detail_Cycle_cnc"];
                press.Apearance_inspect = Request.Form["Detail_Apearance_inspect"];
                press.Laser = Request.Form["Detail_Laser"];
                press.Apearance_inspect = Request.Form["Detail_Partnum"];
                press.Pre_wash = Request.Form["Detail_Pre_wash"];
                press.Wash_cycle = Request.Form["Detail_Wash_cycle"];
            }

         
            var update1 = _cap.EditPressModels(press); 
            var update2 = UpdateGroupCapacitySummary(capid, Request.Form["ProcessCode"]);

            bool[] results = await Task.WhenAll(update1, update2);
            if (!results.All(r => r)) JsonValidationError();

            CacheHelper.Remove("Press");
            await UpdateRepository.UpdateUserLogs(7, 1, "Add");
            return JsonCreated(press, "Update Successfully");
        }



        [HttpPost]
        public async Task<ActionResult> DeleteModelsData()
        {
            int capinfo = Convert.ToInt32(Request.Form["Capinfo_ID"]);
            int capgroup = Convert.ToInt32(Request.Form["Capgroup_ID"]);


            //var update1 = _cap.DeleteModels(capinfo, capgroup);
            //var update2 = UpdateGroupCapacitySummary(capid, Request.Form["ProcessCode"]);

            //bool[] results = await Task.WhenAll(update1, update2);

            var formdata = GlobalUtilities.GetMessageResponse(await _cap.DeleteModels(capinfo, capgroup), 1);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        // ====================================================================================
        // ================= FUNCTION FOR UPDATING ============================================
        // ====================================================================================
        public async Task<bool> UpdateGroupCapacitySummary(int  capid, string proccessCode)
        {
            string month = DateTime.Now.ToString("MMMM");
            int totalmachoine = 0;
            int totalcapday = 0;
            int totalcap = 0;
            int totalMan = 0;
            int processManpower = 0;

            var getdata = await _cap.GetCapacitySummary(month, capid) ?? new List<CapacitySummaryModel>();

            foreach (var item in getdata)
            {       
                totalmachoine += item.AvailMachine;
                totalcapday += item.Capday;
                totalcap += item.Capmonth;
                totalMan += item.RequiredMan;

                if(item.ProcessCode == proccessCode)
                {
                    processManpower = item.RequiredMan;
                }
            }


            var upobj = new CapacityGroupPostModel
            {
                Total_machine = totalmachoine,
                Capday = totalcapday,
                Capmonth = totalcap,
                TotalMan = totalMan,
                Capgroup_ID = capid
            };

            var update1 = _cap.UpdateCapacityGroup(upobj);
            var update2 = _cap.UpdateManpower(processManpower, proccessCode);

            bool[] results = await Task.WhenAll(update1, update2);

            CacheHelper.Remove("Capsummary");

            return results.All(r => r);
        }
    }
}