using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Controllers;
using PMACS_V2.Utilities;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PMACS_V2.Areas.P1SA.Controllers
{
    [CompressResponse]
    public class DieMoldController : ExtendController
    {
        private readonly IDieMold _die;
        public DieMoldController(IDieMold die) => _die = die;

        // ===========================================================
        // ==================== MOLD DIE DATA  =======================
        // ===========================================================
        public async Task<ActionResult> GetMoldDieSummaryList(string ProcessID)
        {
            try
            {
                var data = await _die.GetMoldDieSummary(ProcessID);
                if (data == null || !data.Any())
                    return JsonNotFound("No DieSummary  data not found");

                // Get the Max no of the data
                int maxNo = data.Any() ? data.Max(x => x.No) : 0;

                // Get the Total Count base on remarks
                int monitor = data.Count(x => x.Remarks == "For Monitoring");
                int Endlife = data.Count(x => x.Remarks == "End of Life");
                int Maxdie = maxNo - (monitor + Endlife);

                List<FinalMoldDieSummary> item = new List<FinalMoldDieSummary>();   

                for(int i = 1; i <= 3; i++)
                {
                    if(i == 1)
                    {
                        item.Add(new FinalMoldDieSummary
                        {
                            Category = "Max Die life",
                            MoldDie = Maxdie
                        });
                    }else if (i == 2)
                    {
                        item.Add(new FinalMoldDieSummary
                        {
                            Category = "For Monitoring",
                            MoldDie = monitor,
                        });
                    }
                    else
                    {
                        item.Add(new FinalMoldDieSummary
                        {
                            Category = "End of life",
                            MoldDie = Endlife,
                        });
                    }
                    
                }

                var dataSets = new Dictionary<string, IEnumerable<object>>
                {
                    { "FinalSummary", data },
                    { "MoldDieSummary", item }
                };


                return JsonMultipleData(dataSets);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetMoldDieMonthInputList(int Months, int Year, string ProcessID)
        {
            try
            {          
                var data = await _die.GetMoldDieMonthInput(Months, Year, ProcessID);

                if (data == null || !data.Any())
                    return JsonNotFound("No DieMonth input data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddUpdateMoldDieMonitor(MoldInputModel add)
        {
            bool update = await _die.AddUpdateMoldie(add);
            var formdata = GlobalUtilities.GetMessageResponse(update, 1);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }
        // ===========================================================
        // ==================== PRESS MOLD DIE DATA  ==================
        // ===========================================================
        public async Task<ActionResult> GetPressDieRegistryList()
        {
            try
            {
                var data = await _die.GetPressRegistryList();

                if (data == null || !data.Any())
                    return JsonNotFound("No DieMonth input data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetPressDieMonitoringList()
        {
            try
            {
                var data = await _die.GetPressMonitoring();

                if (data == null || !data.Any())
                    return JsonNotFound("No Monitoring data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }
        public async Task<ActionResult> GetPressDieSummaryList()
        {
            try
            {
                var data = await _die.GetPressSummary();

                if (data == null || !data.Any())
                    return JsonNotFound("No Monitoring data found");

                return JsonSuccess(data);
            }
            catch (Exception ex)
            {
                return JsonError(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> AddUpdatePressDieMonitor(PressInputModel add)
        {
            //bool update = await _die.AddUpdatePressMonitoring(add);
            //var formdata = GlobalUtilities.GetMessageResponse(update, 1);
            return Json(add, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> AddPressRegistry(PressDieRegistry obj)
        {
            bool update = await _die.AddPressRegistry(obj);
            var formdata = GlobalUtilities.GetMessageResponse(update, 0);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePressRegistry()
        {
            var obj = new PressDieRegistry
            {
                ToolNo = Request.Form["EditToolNo"],
                Type = Request.Form["EditType"],
                Model = Request.Form["EditModel"],
                Lines = Convert.ToInt32(Request.Form["EditLine"]),
                Status = Request.Form["EditStatus"],    
                Operational = Convert.ToInt32(Request.Form["EditOpe"])
            };

            bool update = await _die.EditPressRegistry(obj);
            var formdata = GlobalUtilities.GetMessageResponse(update, 1);
            return Json(formdata, JsonRequestBehavior.AllowGet);
        }

        // GET: P1SA/DieMold
        public ActionResult DieMoldLife() =>  View();
        public ActionResult DiePressLife() => View();

    }
}