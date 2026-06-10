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
        Task<List<FanTraceabilityManufacturingOrder>> GetListofShopOrdersByLine(
            string Linename, string searchtext = "", int orderstatus = 0);

        Task<FanTraceabilityManufacturingOrder> GetShopderDetails(int id);
        Task<string> GetAlreadyDoneShopOrdersBySection(string finalorder);
        Task<bool> SelectOnlineShopOrders(int recordID);
        Task<bool> ChangeLineShopOrder(int recordID, string Lineselect);

        Task<bool> NextModelProcess(int id, string newLine);
        Task UploadDataToDatabase(ProductionRecord model);
        Task<bool> UpdateStatusShopOrder(int id, int status);

        Task<bool> CheckIfNextInprocessExist(string line);
        Task<bool> CheckCurrentStatusChange(int record);

        Task<List<string>> GetListLine();


        Task<int> GetNumberofNextprocess(string record);
        Task<bool> UpdateAssemblyStatus(int recordID, string finalassy, DateTime shipdate, string mode, bool WithSR,  string remarks);
        Task<List<P1TraceablityModel>> TraceableShopOrderSummary(string shopOrder);
    }
}
