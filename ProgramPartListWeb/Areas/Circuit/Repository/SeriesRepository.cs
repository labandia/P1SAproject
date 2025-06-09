using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Interfaces;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Data
{
    public class SeriesRepository : ISeriesRepository
    {
        

        // ###################   PULL THE DATA SERIES  ##################################
        public async Task<List<SeriesviewModel>> GetSeriesData()
        {
            string strquery = "SELECT s.Series_ID, s.Series_no, s.Line, s.Modelno, " +
                             "s.timetarget, s.createdBy, s.Shift, s.Remarks, " +
                             "s.SetupNavi, s.VisualManage, s.Status,  " +
                             "s.MachineSerial, s.SetGroup, s.Ongoing, " +
                             "FORMAT(s.DateCreated, 'MM/dd/yyyy') as DateCreated, " + 
                             "(SELECT COUNT(p.Series_ID) FROM PartList_Prepare_tbl p " +
                             "WHERE p.Series_ID = s.Series_ID) as TotalCount, " +
                             "Case WHEN COALESCE((SELECT COUNT(Series_ID) as Parts " +
                            "FROM PartList_Coms_Summary_tbl " +
                            "WHERE Series_ID = s.Series_ID " +
                            "GROUP BY Series_ID), 0) >= COALESCE((SELECT COUNT(Series_ID) as Parts " +
                            "FROM PartList_Prepare_tbl " +
                            "WHERE Series_ID = s.Series_ID " +
                            "GROUP BY Series_ID), 0)  THEN 1 " +
                            "ELSE 0 END AS Planstatus " +
                             "FROM PartList_Series_tbl s ORDER BY Series_ID DESC"; 
            return await SqlDataAccess.GetData<SeriesviewModel>(strquery, null, "serieslist");
        }
        public async Task<List<PrepareviewModel>> GetComponentsList(int intseries)
        {
            string strquery = "Getpartscomponents";
            var parameters = new { seriesID = intseries };
            return await SqlDataAccess.GetData<PrepareviewModel>(strquery, parameters);
        }
        public async Task<List<WarehousePreparedModel>> GetWarehousePreparedData(int intseries)
        {
            string strquery = "WarehousePrepared";
            var parameters = new { seriesID = intseries };
            return await SqlDataAccess.GetData<WarehousePreparedModel>(strquery, parameters, "PreparedWarehouse");
        }


        // ################### COMPONENTS SUMMARY DISPLAY ###################################
        public async Task<List<SummaryComponentModel>> GetComponentsSummmary(string strval)
        {
            string strquery = "ComponentsSummary";
            var parameters = new { Series_no = strval};
            return await SqlDataAccess.GetData<SummaryComponentModel>(strquery, parameters, "GetComponentsSummary");
        }
        public async Task<List<PartlistModel>> Getpartlist(string series)
        {
            string strquery = "PartlistData";
            var parameters = new { Series_no = series };
            return await SqlDataAccess.GetData<PartlistModel>(strquery, parameters, "PartlistData");
        }
        //public async Task<List<SeriesviewModel>> GetSeriesDataTable(int pagenum, int pagesize, int filter)
        //{
           
        //    string strfilter = filter == 2 ? "" : "WHERE s.Ongoing = " + filter + "";
        //    string strquery = "SELECT " +
        //               "s.Series_ID, s.Series_no, s.Line, s.Modelno, " +
        //               "s.timetarget, s.createdBy, s.Shift, s.Remarks, " +
        //               "s.SetupNavi, s.VisualManage, s.Status, " +
        //               "s.MachineSerial, s.SetGroup, s.Ongoing, " +
        //               "(SELECT COUNT(p.Series_ID) " +
        //                 "FROM PartList_Prepare_tbl p " +
        //                 "WHERE p.Series_ID = s.Series_ID) as TotalCount " +
        //               "FROM PartList_Series_tbl s " +
        //               strfilter +
        //               "ORDER BY Series_ID DESC " +
        //               "OFFSET (@PageNumber - 1) * @PageSize " +
        //               "ROWS   FETCH NEXT @PageSize ROWS ONLY  ";


        //    var parameters = new { PageNumber = pagenum, PageSize = pagesize };
        //    return await SqlDataAccess.GetData<SeriesviewModel>(strquery, parameters);
        //}
        public async Task<int> GetTotalQuantity(string partnum, int seriesID)
        {
            string strquery = "SummaryCount";
            var parameters = new { AbassadorPartnum = partnum, @Series_ID = seriesID };
            return await SqlDataAccess.GetCountData(strquery, parameters);
        }
        public async Task<bool> UpdatePreparedQuantity(object parameters)
        {
            string strquery = "UPDATE PartList_Coms_Summary_tbl SET Stats =@Stats " +
              "WHERE Series_ID =@Series_ID AND AbassadorPartnum =@AbassadorPartnum";
            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
            return result;
        }
        public async Task<bool> UpdateSummaryQuantity(object parameters)
        {
            string strquery = "UPDATE PartList_Prepare_tbl SET Prepared_Quantity =@Prepared_Quantity " +
                "WHERE Series_ID =@Series_ID AND AbassadorPartnum =@AbassadorPartnum";
            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
            return result;
        }


        public async Task<bool> UpdatePartsSummary(int series, string part, int quan)
        {
            bool result = false;
            string strcheck = "SELECT RecordID FROM PartList_ComponentStats_tbl " +
                              "WHERE Series_ID = @Series_ID AND AbassadorPartnum = @AbassadorPartnum ";
            var charparams = new { Series_ID = series, AbassadorPartnum = part };
            bool checkres = await SqlDataAccess.Checkdata(strcheck, charparams);

            if (checkres)
            {
                string strquery = "UPDATE PartList_ComponentStats_tbl SET Quantity =@Quantity " +
                   "WHERE Series_ID =@Series_ID AND AbassadorPartnum =@AbassadorPartnum";
                var parameters = new { Series_ID = series, Quantity = quan, AbassadorPartnum = part };
                 result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
            }
            else
            {
                string strquery = "INSERT INTO PartList_ComponentStats_tbl(Quantity, Series_ID, AbassadorPartnum)" +
                        " VALUES(@Quantity, @Series_ID, @AbassadorPartnum)";

                var parameters = new { Series_ID = series, Quantity = quan, AbassadorPartnum = part };
                result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
            }
       
            return result;
        }



        public async Task<bool> SaveSummaryComponents(object parameters)
        {  
            string strquery = "INSERT INTO PartList_Coms_Summary_tbl(Series_ID, NeedQuan, ReelID, " +
                              "AbassadorPartnum, CompIN, SetNo, Stats, SupID, LotNo) VALUES(@SeriesID, @NeedQuan, @ReelID, " +
                              "@AbassadorPartnum, @CompIN, @SetNo, @Stats, @SupID, @LotNo)";

            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
            return result;
        }

        public async Task<bool> DeleteSummaryComponentList(object parameters)
        {
            string strquery = "DELETE FROM PartList_Coms_Summary_tbl  WHERE AbassadorPartnum =@AbassadorPartnum";
            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
            return result;
        }


        public async Task<int> GetSeriesID(string strSeries)
        {
            var seriesData = await GetSeriesData();

            var series = seriesData.FirstOrDefault(res => res.Series_no.Equals(strSeries, StringComparison.OrdinalIgnoreCase));

            return series?.Series_ID ?? 0;
        }
        public async Task<bool> SeriesChangeStatus(int Id, int stats)
        {
            string query = "UPDATE PartList_Series_tbl SET Ongoing =@Ongoing WHERE Series_ID =@Series_ID";
            var parameters = new { Ongoing = Id, Series_ID = stats };

            bool result = await SqlDataAccess.UpdateInsertQuery(query, parameters);

            return result;
        }
        // ###################   ADD NEW DATA FOR THE SERIES ##################################
        public async Task<bool> AddSeriesData(object parameters)
        {    
            string strquery = "INSERT INTO PartList_Series_tbl " +
                              "(Series_no, Line, Timetarget, CreatedBy, " +
                              "Shift, Remarks, SetupNavi, VisualManage, Status, " +
                              "MachineSerial, Modelno, SetGroup) " +
                              "VALUES (@Series_no, @Line, @Timetarget, @CreatedBy, " +
                              "@Shift, @Remarks, @SetupNavi, " +
                              "@VisualManage, @Status, @MachineSerial, " +
                              "@Modelno, @SetGroup)";
            return await SqlDataAccess.UpdateInsertQuery(strquery, parameters, "serieslist"); 
        }

        public async Task<bool> UpdateSeriesData(object parameters)
        {
            string strquery = "UPDATE PartList_Series_tbl SET " +
                              "Series_no = @Series_no, Line =@Line, Timetarget =@Timetarget, CreatedBy =@CreatedBy, " +
                              "Remarks =@Remarks, SetupNavi =@SetupNavi, VisualManage =@VisualManage, Status =@Status, " +
                              "MachineSerial =@MachineSerial, Modelno =@Modelno, SetGroup =@SetGroup " +
                              "WHERE Series_ID =@Series_ID";
            return await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
        }



        // ###################  IMPORT EXCEL FILE AND SAVE TO DATABASE ##################################
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

                    if (string.IsNullOrWhiteSpace(row["SetNo"]))
                    {
                        Debug.WriteLine("SetNo is empty. Stopping loop.");
                        break; // Stop the loop if SetNo is empty
                    }
              
                    //Debug.WriteLine(row["FdrType"] + " " + row["PitchIndex"]);
                    var parlistobj = new PartlistPostModel
                    {
                        Series_ID = seriesID,
                        SetNo = Convert.ToInt32(row["SetNo"]),
                        AbassadorPartnum = row["CompName"],
                        FeederType = row["FdrType"] + " " + row["PitchIndex"],
                        Prepared_Quantity = Convert.ToInt32(row["Total"]),
                        Machno = intmach
                    };


                    string query = "INSERT INTO PartList_Prepare_tbl (Series_ID, SetNo, AbassadorPartnum, FeederType, Prepared_Quantity, Machno) " +
                        "VALUES(@Series_ID, @SetNo, @AbassadorPartnum, @FeederType, @Prepared_Quantity, @Machno)";

                    bool result = await SqlDataAccess.UpdateInsertQuery(query, parlistobj);
                    //Debug.WriteLine(result);
                    if (!result)
                    {
                        finalresult = false;
                        break;
                    }
                }

            }
            else
            {
                finalresult = false;
            }
            //Debug.WriteLine("######################## END LOOP ##########################");
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

            }
            return finalresult;
        }

        public async Task<bool> UploadComponentsPartlistOut(List<Dictionary<string, string>> rows, int seriesID)
        {
            bool result = false;

           
            foreach (var row in rows)
            {
                string input = row["ShootDate"];
                string strdate = input.Replace("_", " ");
               
                if (row["ProductName"] == "")
                {
                    continue;
                }
                else
                {
                    // get the main string comp name
                    string compname = row["ProductName"];
                    //get the part number
                    string partname = compname.Substring(3, compname.IndexOf(' ') - 3);
                    //get the reel id
                    string strReel = compname.Substring(compname.IndexOf(' ') + 1);


                    // Checks if the Reel ID exist in the PartList_Coms_Summary_tbl
                    string checkcoms = "SELECT COUNT(ReelID) as ReelID FROM PartList_Coms_Summary_tbl " +
                                       "WHERE Series_ID = @Series_ID AND ReelID = @ReelID ";
                    //Debug.WriteLine($"Series ID : {seriesID}, Reel ID : {strReel}, Part name : {partname}");


                    var checkparams = new {
                        Series_ID = seriesID,
                        ReelID = strReel
                    };



                    if (await SqlDataAccess.Checkdata(checkcoms, checkparams))
                    {

                        string strquery = "UPDATE PartList_Coms_Summary_tbl SET CompOut =@CompOut " +
                                        "WHERE AbassadorPartnum =@AbassadorPartnum AND ReelID =@ReelID";
                        var parameters = new
                        {
                            CompOut = Convert.ToInt32(row["ChipCount"]),
                            ReelID = strReel,
                            AbassadorPartnum = partname
                        };


                        result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
                    }
                    else
                    {

                        string strquery = "INSERT INTO PartList_Coms_Summary_tbl(Series_ID, ReelID, " +
                             "AbassadorPartnum, CompOut) VALUES(@SeriesID, @ReelID, " +
                             "@AbassadorPartnum, @CompOut)";
                        var parameters = new
                        {
                            SeriesID = seriesID,
                            CompOut = Convert.ToInt32(row["ChipCount"]),
                            ReelID = strReel,
                            AbassadorPartnum = partname
                        };
                        result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
                    }

                    // INSERT TO THE OTHER TABLE FOR THE CHECKING OF UPLOAD
                    string insertquery = "INSERT INTO PartList_ReturnOut_tbl(ShootDate, ProductName, ChipCount, Series_ID) " +
                        "VALUES(@ShootDate, @ProductName, @ChipCount, @Series_ID)";
                    var insertparams = new
                    {
                        ShootDate = strdate,
                        ProductName = row["ProductName"],
                        ChipCount = Convert.ToInt32(row["ChipCount"]),
                        Series_ID = seriesID
                    };
                    result = await SqlDataAccess.UpdateInsertQuery(insertquery, insertparams);



                }
                
            }

            return result;
        }

        public async Task<List<ReturnModel>> GetComponentsOutData()
        {
            string strquery = "SELECT  r.ProductName AS ComponentsName, r.ChipCount AS Quantity, w.ItemCode, " +
	                          "SUBSTRING(r.ProductName, 4, CHARINDEX(' ', r.ProductName) - 4) as Ambassador, " +
	                          "SUBSTRING(r.ProductName, CHARINDEX(' ', r.ProductName) + 1, LEN(r.ProductName)) as Reel_ID " +
                              "FROM PartList_ReturnOut_tbl r " +
                              "LEFT JOIN PartList_Masterlist_Warehouse w ON " +
                              "w.AbassadorPartnum = SUBSTRING(r.ProductName, 4, CHARINDEX(' ', r.ProductName) - 4)";
            return await SqlDataAccess.GetData<ReturnModel>(strquery);
        }

        public async Task<bool> CheckComponentsOutData(int ID)
        {
            string strquery = "SELECT COUNT(Series_ID) as SeriesID FROM PartList_ReturnOut_tbl " +
                              "WHERE Series_ID = @Series_ID";
            var parameter = new { Series_ID = ID };
            return await SqlDataAccess.Checkdata(strquery, parameter);
        }

        public async Task<List<SummaryHistory>> GetHistoryTransactionData()
        {
            string strquery = "SELECT s.Series_no, CONCAT('SDP', cs.AbassadorPartnum, ' ', cs.ReelID) as ProductName, " +
	                          "cs.AbassadorPartnum, w.ItemCode, cs.NeedQuan, " +
	                          "cs.CompIN, cs.CompOut,  cs.CompIN - cs.NeedQuan as Totalprod, " +
	                          "cs.CompOut - cs.CompIN - cs.NeedQuan as Diff " +
                          "FROM  PartList_Coms_Summary_tbl cs " +
                          "INNER JOIN PartList_Series_tbl s ON s.Series_ID = cs.Series_ID " +
                          "LEFT JOIN PartList_Masterlist_Warehouse w ON w.AbassadorPartnum = cs.AbassadorPartnum";
            return await SqlDataAccess.GetData<SummaryHistory>(strquery);
        }

        public async Task<List<SuppliersModel>> GetSuppliersData(string partnum)
        {
            string strquery = "SELECT SupID, AbassadorPartnum, Partname, Supplier, Code " +
                              "FROM PartList_Suppliers_tbl WHERE AbassadorPartnum =@AbassadorPartnum";
            var parameters = new { AbassadorPartnum = partnum };
            return await SqlDataAccess.GetData<SuppliersModel>(strquery, parameters);
        }

        public async Task<List<SupplierModel>> GetSupplierData()
        {
            string strquery = "SupplierData";
            return await SqlDataAccess.GetData<SupplierModel>(strquery, null, "Suppliers");
        }

        public async Task<bool> AddEditSuppliers(SupplierModel sup)
        {
            string strquery = sup.SupID != 0 ? "EditSupplierData" : "AddSupplierData";
            dynamic obj;

            if (sup.SupID == 0)
            {
                obj = new
                {
                    _AbassadorPartnum = sup.AbassadorPartnum,
                    _Partname = sup.Partname,
                    _Supplier = sup.Supplier,
                    _Code = sup.Code
                };
            }
            else
            {        
                obj = new
                {
                    _AbassadorPartnum = sup.AbassadorPartnum,
                    _Partname = sup.Partname,
                    _Supplier = sup.Supplier,
                    _Code = sup.Code,
                    _SupID = sup.SupID
                };
            }

            return await SqlDataAccess.UpdateInsertQuery(strquery, obj);
        }

       
    }
}