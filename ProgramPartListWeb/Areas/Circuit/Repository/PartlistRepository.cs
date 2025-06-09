
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Areas.Circuit.Repository
{
    public class PartlistRepository 
    {
  
        public PartlistRepository()
        {
        }

        //----------------------- PULL THE DATA SERIES -----------------------------
        // SERIES DATA
        public async Task<IEnumerable<SeriesviewModel>> GetSeriesData()
        {
            string strquery = "SELECT s.Series_ID, s.Series_no, s.Line, s.Modelno, " +
                              "s.timetarget, s.createdBy, s.Shift, s.Remarks, " +
                              "s.SetupNavi, s.VisualManage, s.Status, " +
                              "s.MachineSerial, s.SetGroup, s.Ongoing, " +
                              "FORMAT(s.DateCreated, 'MM/dd/yyyy') as DateCreated, " +
                              "(SELECT COUNT(p.Series_ID) " +
                                "FROM PartList_Prepare_tbl p " +
                                "WHERE p.Series_ID = s.Series_ID) as TotalCount " +
                              "FROM PartList_Series_tbl s";
            return await SqlDataAccess.GetData<SeriesviewModel>(strquery);
        }
        public async Task<IEnumerable<SeriesviewModel>> GetSeriesDataTable(int pagenum, int pagesize, int filter)
        {
            string strquery;
            string strfilter = filter == 2 ? "" : "WHERE s.Ongoing = " + filter + "";

            strquery = "SELECT " +
                       "s.Series_ID, s.Series_no, s.Line, s.Modelno, " +
                       "s.timetarget, s.createdBy, s.Shift, s.Remarks, " +
                       "s.SetupNavi, s.VisualManage, s.Status, " +
                       "s.MachineSerial, s.SetGroup, s.Ongoing, " +
                       "(SELECT COUNT(p.Series_ID) " +
                         "FROM PartList_Prepare_tbl p " +
                         "WHERE p.Series_ID = s.Series_ID) as TotalCount " +
                       "FROM PartList_Series_tbl s " +
                       strfilter +
                       "ORDER BY Series_ID ASC " +
                       "OFFSET (@PageNumber - 1) * @PageSize " +
                       "ROWS   FETCH NEXT @PageSize ROWS ONLY  ";


            var parameters = new { PageNumber = pagenum, PageSize = pagesize };
            return await SqlDataAccess.GetData<SeriesviewModel>(strquery, parameters);
        }
        public async Task<IEnumerable<SeriesviewModel>> GetSeriesDetailsData(string series)
        {
            string strquery = "SELECT Series_ID, Series_no, Line, Modelno, timetarget, createdBy, Shift, Remarks, " +
                              "SetupNavi, VisualManage, Status, MachineSerial, SetGroup, Ongoing " +
                              "FROM PartList_Series_tbl WHERE Series_no = '"+ series +"'";
            return await SqlDataAccess.GetData<SeriesviewModel>(strquery);
        }
        public async Task<int> GetSeriesID(string strSeries)
        {
            var seriesData = await GetSeriesData();

            var series = seriesData.FirstOrDefault(res => res.Series_no.Equals(strSeries, StringComparison.OrdinalIgnoreCase));

            return series?.Series_ID ?? 0;
        }
        // PREPARED DATA
        public async Task<IEnumerable<PrepareviewModel>> GetComponentsList(int intseries)
        {
            string strquery = "Getpartscomponents";
            var parameters = new { seriesID = intseries };
            return await SqlDataAccess.GetData<PrepareviewModel>(strquery, parameters);
        }
        public async Task<IEnumerable<PrepareviewModel>> GetPartnamelist(string strpartnum, int intseries)
        {
            // SEARCH BY SERIES PARTLIST PARTNUMBER 
            string strquery = " SELECT p.RecordID, s.Series_no, w.AbassadorPartnum, " +
                            "p.SetNo, w.Partname, w.Locations, p.FeederType, " +
                            "p.Prepared_Quantity, p.Machno " +
                            "FROM PartList_Prepare_tbl p " +
                            "INNER JOIN PartList_Series_tbl  s  " +
                            "ON s.Series_ID = p.Series_ID " +
                            "INNER JOIN PartList_WarehouseLocation_tbl w  " +
                            "On p.AbassadorPartnum = w.AbassadorPartnum " +
                            "WHERE w.AbassadorPartnum = @searchText AND s.Series_ID = @seriesID";
            var parameters = new { searchText = strpartnum, seriesID = intseries };
            return await SqlDataAccess.GetData<PrepareviewModel>(strquery, parameters);
        }
        public async Task<IEnumerable<SummaryComponentModel>> GetComponentsSummmary(string series)
        {
            //COMPONENTS SUMMARY DISPLAY
            string strquery = "ComponentsSummary";
            var parameters = new { Series_no = series };
            return await SqlDataAccess.GetData<SummaryComponentModel>(strquery, parameters);
        }
        public async Task<int> GetTotalQuantity(string partnum, int seriesID)
        {
            // GET THE TOTAL COUNT OF SUMMARY COMPONENTS QUANTITY INPUT
            string strquery = "SummaryCount";
            var parameters = new { AbassadorPartnum = partnum, @Series_ID = seriesID };
            return await SqlDataAccess.GetCountData(strquery, parameters);
        }
        public async Task<IEnumerable<PartlistModel>> Getpartlist(string series)
        {
            // COMPONENTS PARTLIST DISPLAY
            string strquery = "PartlistData";
            var parameters = new { Series_no = series };
            return await SqlDataAccess.GetData<PartlistModel>(strquery, parameters);
        }
        // WAREHOUSE DATA
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




        //----------------------- UPDATE AND ADD DATA SERIES -----------------------
        // SERIES ADD AND UPDATE
        public async Task<bool> UpdateSeriesStatus(int Id, int stats)
        {
            string query = "UPDATE PartList_Series_tbl SET Ongoing =@Ongoing WHERE Series_ID =@Series_ID";
            var parameters = new { Ongoing = Id, Series_ID = stats };

            bool result = await SqlDataAccess.UpdateInsertQuery(query, parameters);

            return result;
        }
        public async Task<bool> AddSeriesData(object parameters)
        {
            bool result;
            //string strcheckseries = "CheckSeriesno";
            //dynamic param = parameters;

            //var seriesparams = new { Series_no = param.Series_no };

            //if (await _series.Checkdata(strcheckseries, seriesparams))
            //{

            //}
            //else
            //{

            //}
            string strquery = "INSERT INTO PartList_Series_tbl " +
                              "(Series_no, Line, Timetarget, CreatedBy, " +
                              "Shift, Remarks, SetupNavi, VisualManage, Status, " +
                              "MachineSerial, Modelno, SetGroup) " +
                              "VALUES (@Series_no, @Line, @Timetarget, @CreatedBy, " +
                              "@Shift, @Remarks, @SetupNavi, " +
                              "@VisualManage, @Status, @MachineSerial, " +
                              "@Modelno, @SetGroup)";

            result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);

            return result;
        }
        // PREPARED ADD AND UPDATE
        public async Task<bool> SaveSummaryComponents(object parameters)
        {
            string strquery = "INSERT PartList_Components_Summary_tbl(Series_ID, ComponentName, Quantity, LotID, " +
                              "AbassadorPartnum, QuantityInput, Machine, SetNo, Stats) VALUES(@SeriesID, @ComponentName, " +
                              "@Quantity, @LotID, @Partnum, @QuanIn, @Machine, @SetNo, @Stats)";

            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);

            return result;
        }
        public async Task<bool> UpdatePreparedQuantity(object parameters)
        {
            string strquery = "UPDATE PartList_Components_Summary_tbl SET Stats =@Stats " +
                "WHERE Series_ID =@Series_ID AND AbassadorPartnum =@AbassadorPartnum";
            Debug.WriteLine(strquery);
            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);

            return result;
        }
        // WAREHOUSE ADD 
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

        //----------------------- IMPORT EXCEL FILE AND SAVE TO DATABASE -----------
        public async Task<bool> SaveImportData(List<Dictionary<string, string>> rows, string strseries, int intmach)
        {
            int counter = 0;
            bool finalresult = true;

            int seriesID = await GetSeriesID(strseries);

            if (seriesID != 0)
            {
                foreach (var row in rows)
                {
                    counter++;

                    var feeder = row["FdrType"] + " " + row["PitchIndex"];
                    var total = Convert.ToInt32(row["Total"].ToString());

                    string query = "INSERT INTO PartList_Prepare_tbl (Series_ID, SetNo, AbassadorPartnum, FeederType, Prepared_Quantity, Machno) VALUES(@Series_ID, @SetNo, @AbassadorPartnum, @FeederType, @Prepared_Quantity, @Machno)";
                    var parameters = new { Series_ID = seriesID, SetNo = row["SetNo"], AbassadorPartnum = row["CompName"], FeederType = feeder, Prepared_Quantity = total, Machno = intmach };

                    bool result = await SqlDataAccess.UpdateInsertQuery(query, parameters);

                    if (!result)
                    {
                        finalresult = false;
                        break;
                    }

                    //Console.WriteLine($"Values: {row["CompName"]}");
                    //Console.WriteLine();
                }

            }
            else
            {
                finalresult = false;
            }

            return finalresult;
        }
        public async Task<bool> LateSaveImportData(List<Dictionary<string, string>> rows, int series, int intmach)
        {
            int counter = 0;
            bool finalresult = true;


            foreach (var row in rows)
            {
                counter++;

                var feeder = row["FdrType"] + " " + row["PitchIndex"];
                var total = Convert.ToInt32(row["Total"].ToString());

                string query = "INSERT INTO PartList_Prepare_tbl (Series_ID, SetNo, AbassadorPartnum, FeederType, Prepared_Quantity, Machno) VALUES(@Series_ID, @SetNo, @AbassadorPartnum, @FeederType, @Prepared_Quantity, @Machno)";
                var parameters = new { Series_ID = series, SetNo = row["SetNo"], AbassadorPartnum = row["CompName"], FeederType = feeder, Prepared_Quantity = total, Machno = 1 };

                bool result = await SqlDataAccess.UpdateInsertQuery(query, parameters);

                if (!result)
                {
                    finalresult = false;
                    break;
                }

                //Console.WriteLine($"Values: {row["CompName"]}");
                //Console.WriteLine();
            }
            return finalresult;
        }

        //----------------------- TOTAL COUNT PART PROGRAM PART LIST ---------------
        public async Task<int> GetCountPartlist(string series)
        {
            string strquery = "Countpartlist";
            var parameters = new { Series_no = series };
            return await SqlDataAccess.GetCountData(strquery, parameters);
        }
        //----------------------- CHECK IF THE SERIES NO IS ALREADY INSERT IN PARTLIST ------
        public async Task<bool> CheckSeriesPartlist(string series)
        {
            string strquery = "Checkpartlist";
            var parameters = new { seriescheck = series };
            return await SqlDataAccess.Checkdata(strquery, parameters);
        }
    }
}