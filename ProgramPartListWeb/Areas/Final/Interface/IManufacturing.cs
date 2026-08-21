using ProgramPartListWeb.Areas.Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProgramPartListWeb.Areas.Final.Model.DisposalModels;

namespace ProgramPartListWeb.Areas.Final
{
    public interface IManufacturing
    {
        Task AutoUpdateShopOrderLine();

        Task<List<FanTraceabilityManufacturingOrder>> GetListofActiveShopOrders();
        Task<int> GetCountShopOrders(string line);
        Task<List<FanTraceabilityManufacturingOrder>> GetListofShopOrdersByLine(
            string Linename, string searchtext = "", int orderstatus = 0);

        Task<int> GetActualCountOfShopOrders(string Linename);

        Task<FanTraceabilityManufacturingOrder> GetShopderDetails(int id);
        Task<string> GetAlreadyDoneShopOrdersBySection(string finalorder);
        Task<bool> SelectOnlineShopOrders(int recordID);
        Task<bool> ChangeLineShopOrder(int recordID, string Lineselect, int process);
        Task<bool> AddInputQuantiyPerLine(int recordID, int Qty);

      

        // ======  UPDATING STATUS OF SHOP ORDER DATA =================
        Task<bool> UpdateStatusShopOrder(int id, int status, string line);
        Task<bool> UpdateCompleteShopOrder(int id, int status, string line);
        Task<bool> CompletionStatusShopOrder(int id, int status, string line);
        Task<bool> NextModelProcess(string newLine);
        Task<bool> CancelProcess(int id, string line);
        // ============================================================

        // ======  FOR UPLOAD DATA  ====================================
        Task UploadDataToDatabase(ProductionRecord model);

        Task<bool> CheckIfNextInprocessExist(string line);
        Task<bool> CheckCurrentStatusChange(int record);

        Task<List<string>> GetListLine();


        Task<int> GetNumberofNextprocess(string record);
        Task<bool> UpdateAssemblyStatus(int recordID, string finalassy, DateTime shipdate, string mode, bool WithSR,  string remarks);
        Task<List<P1TraceablityModel>> TraceableShopOrderSummary(string shopOrder);

        // ====== PARTLY SHORT DATA SUMMARY REPORT =================
        Task<List<AssemblyPartlistRecord>> GetPartlyShortSummary(int isdispatch);
        Task<List<DispatchPartlistRecord>> GetDispatchShortSummay();
        Task<(int PlanQty, string LastDate, decimal totalpercent)> GetLastUpdateAndTotal(int isdispatch);


        // ============================================================

        // ====== MEIG DATA ======================
        Task<List<CatergoryPartsModel>> GetCategoryRegistration(int category);
        Task<List<MEIGpartsModel>>  GetRegistrationMEIG(string finashopOrder);
        Task<bool> AddRegistrationMEIG(MEIGpartsModel mdodel);





        // ====== SEPARATE FOR THE TRACEABILITY =====================
        Task<List<DailyPlanChartModel>> GetDailyPlanChart();


        // ====== FOR THE DISPOSAL SCRAP ============================
        Task<(TotalDisposalMonitor summary, List<DisposalSummary> list)> GetDisposalDetails(int controlID);
        Task<bool> ApproveDisposal(string name,  int id);
        Task<List<string>> GetApproverName(int section);


        // ====== FOR DOWNTIME MONITORING ============================
        Task<List<DownTimeReportModel>> GetDowntimeDailyReport();
        Task<List<DownTimeModel>> GetDowntimeMonitor(string FinalShopOrder);
        Task<List<DownTimeTypeModel>> GetDownTimeType();
        Task<bool> AddGetTimeMonitor(DownTimeModel downtime);

        Task<bool> EndTimeMonitor(int DownTimeID);
    }
}
