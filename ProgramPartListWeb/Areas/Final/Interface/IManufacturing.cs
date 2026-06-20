using ProgramPartListWeb.Areas.Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Final
{
    public interface IManufacturing
    {
        Task AutoUpdateShopOrderLine();

        Task<List<FanTraceabilityManufacturingOrder>> GetListofActiveShopOrders();
        Task<int> GetCountShopOrders(string line);
        Task<List<FanTraceabilityManufacturingOrder>> GetListofShopOrdersByLine(
            string Linename, string searchtext = "", int orderstatus = 0, 
            int page = 1,
            int pageSize = 10);

        Task<FanTraceabilityManufacturingOrder> GetShopderDetails(int id);
        Task<string> GetAlreadyDoneShopOrdersBySection(string finalorder);
        Task<bool> SelectOnlineShopOrders(int recordID);
        Task<bool> ChangeLineShopOrder(int recordID, string Lineselect);
        Task<bool> AddInputQuantiyPerLine(int recordID, int Qty);

      

        // ======  UPDATING STATUS OF SHOP ORDER DATA =================
        Task<bool> UpdateStatusShopOrder(int id, int status, string line);
        Task<bool> CompletionStatusShopOrder(int id, int status, string line);
        Task<bool> NextModelProcess(string newLine);
        // ============================================================

        // ======  FOR UPLOAD DATA  ====================================
        Task UploadDataToDatabase(ProductionRecord model);

        Task<bool> CheckIfNextInprocessExist(string line);
        Task<bool> CheckCurrentStatusChange(int record);

        Task<List<string>> GetListLine();


        Task<int> GetNumberofNextprocess(string record);
        Task<bool> UpdateAssemblyStatus(int recordID, string finalassy, DateTime shipdate, string mode, bool WithSR,  string remarks);
        Task<List<P1TraceablityModel>> TraceableShopOrderSummary(string shopOrder);
    }
}
