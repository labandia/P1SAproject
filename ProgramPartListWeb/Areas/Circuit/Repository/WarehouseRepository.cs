using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Data
{
    public class WarehouseRepository
    {
  
        public WarehouseRepository()
        {
        }

        public async Task<IEnumerable<WarehouseModel>> Warehousepartnumber(string partnum)
        {
            string strquery = "SELECT AbassadorPartnum, Item_name, Reel_Qty, Location, ItemCode, Buyer " +
                              "FROM PartList_Masterlist_Warehouse " + 
                              "WHERE AbassadorPartnum = '" + partnum + "' ";

            return await SqlDataAccess.GetData<WarehouseModel>(strquery);
        }
        public async Task<IEnumerable<WarehouseModel>> Warehousepartsmasterlist()
        {
            string strquery = "SELECT AbassadorPartnum, Item_name, Reel_Qty, Location, ItemCode, Buyer " +
                              "FROM PartList_Masterlist_Warehouse ";

            return await SqlDataAccess.GetData<WarehouseModel>(strquery);
        }
        public async Task<IEnumerable<PartlistrequestModel>> PartsRequestSummary()
        {
            string strquery = "SELECT FORMAT(s.DateCreated, 'MM/dd/yyyy') as DateCreated, " +
                               "s.AbassadorPartnum, m.Item_name, " +
                                "CONCAT('*', s.AbassadorPartnum, '*') as Barcode, " +
                                "CONCAT('*', s.Request_Quantity, '*') as Request_Quantity, " +
                                "m.Location, m.ItemCode " +
                                "FROM PartList_Summary_Warehouse s " +
                                "INNER JOIN PartList_Masterlist_Warehouse m " +
                                "ON s.AbassadorPartnum = m.AbassadorPartnum";

            return await SqlDataAccess.GetData<PartlistrequestModel>(strquery);
        }

        public async Task<bool> SaveSummaryRequestWarehouse(List<WarehouseSummaryModel> data)
        {
            bool finalresult = true;

            if (data == null || !data.Any())
                return false;

            try
            {
                foreach (var item in data)
                {
                    //Debug.WriteLine("Partnum: " + item.AbassadorPartnum);
                    //Debug.WriteLine("Quantity : " + item.Request_Quantity);
                    //Debug.WriteLine("Request by : " + item.Requestby);

                    string query = "INSERT INTO PartList_Summary_Warehouse(AbassadorPartnum, Request_Quantity, Requestby) VALUES(@AbassadorPartnum, @Request_Quantity, @Requestby)";
                    var parameters = new { AbassadorPartnum = item.AbassadorPartnum, Request_Quantity = item.Request_Quantity, Requestby = item.Requestby };

                    bool result = await SqlDataAccess.UpdateInsertQuery(query, parameters);

                    if (!result)
                    {
                        finalresult = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }

            return finalresult;
        }
    }
}