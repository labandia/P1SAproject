using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Data
{
    public class PreparedRepository
    {
   

        public async Task<List<PrepareviewModel>> GetComponentsList(int intseries)
        {
            string strquery = "Getpartscomponents";
            var parameters = new {  seriesID = intseries };
            return await SqlDataAccess.GetData<PrepareviewModel>(strquery, parameters);
        }


        // ###################   SEARCH BY SERIES PARTLIST PARTNUMBER  ##################################
        public async Task<IEnumerable<PrepareviewModel>> GetPartnamelist(string strpartnum, int intseries)
        {
      
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

        // ################### COMPONENTS SUMMARY DISPLAY ###################################
        public async Task<List<SummaryComponentModel>> GetComponentsSummmary(string series)
        {
            string strquery = "ComponentsSummary";
            var parameters = new { Series_no = series };
            return await SqlDataAccess.GetData<SummaryComponentModel>(strquery, parameters);
        }

        // ################### GET THE TOTAL COUNT OF SUMMARY COMPONENTS QUANTITY INPUT ##############
        public async Task<int> GetTotalQuantity(string partnum, int seriesID)
        {
            string strquery = "SummaryCount";
            var parameters = new { AbassadorPartnum = partnum, @Series_ID = seriesID };
            return await SqlDataAccess.GetCountData(strquery, parameters);
        }


        // ################### COMPONENTS PARTLIST DISPLAY ###################################
        public async Task<IEnumerable<PartlistModel>> Getpartlist(string series)
        {
            string strquery = "PartlistData";
            var parameters = new { Series_no = series };
            return await SqlDataAccess.GetData<PartlistModel>(strquery, parameters);
        }

        // ################## SAVE TO THE COMPONENTS SUMMARY #############################################
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
            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);

            return result;
        }

    }
}