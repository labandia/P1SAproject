using ProgramPartListWeb.Areas.Press.Interfaces;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Data
{
    public class PressRepository : IAluminumProducts
    {
        public async Task<List<IssuanceModel>> GetIssuanceHistory()
        {
            string strquery = "GetIssuanceHistoryList";
            return await SqlDataAccess.GetData<IssuanceModel>(strquery);
        }
        public async Task<List<PressMasterlistModel>> GetPressMasterData()
        {
            string strquery = "AluminumMasterlist";
            return await SqlDataAccess.GetData<PressMasterlistModel>(strquery);
        }

        public async Task<List<PressIDNote>> GetIDnoteData()
        {
            string strquery = "SELECT NoteID, Color FROM PartslocatorPress_IDNote";
            return await SqlDataAccess.GetData<PressIDNote>(strquery);
        }

        public async Task<List<PressTransactHistoryModel>> GetPressHistoryTransactionData(int Act)
        {
            string strquery = "AluminumHistorylist";
            var parameters = new { Action = Act };
            return await SqlDataAccess.GetData<PressTransactHistoryModel>(strquery, parameters);
        }

        public async Task<bool> AddNewProducts(AddPressMasterlistModel obj)
        {
            bool result;
            string strmasterlist = "SELECT Model FROM PartslocatorPress_masterlist WHERE Partnum = @Partnumtext";
            var param1 = new { Partnumtext = obj.Partnum };
            string strpartnum = await SqlDataAccess.GetOneData(strmasterlist, param1);
           
            // Checks if the partnumber is not exist in the Masterlist Table
            if (strpartnum == "")
            {
                // Insert New Mastelist 
                string straddMasterlist = "pressaddmasterlist";
                var masterparmas = new
                {
                    _Partnum = obj.Partnum,
                    _Model = obj.Model,
                    _NoteID = obj.NoteID
                };

                // Insert New Storage 
                string straddStorage = "pressaddStoragelist";
                var storageparams = new
                {
                    _Partnum = obj.Partnum,
                    _Racksnum = obj.Racksnum,
                    _Levelnum = obj.Levelnum,
                    _Postnum = obj.Postnum,
                    _Boxnum = obj.Boxnum,
                    _Quantity = obj.Quantity
                };


                Task master = SqlDataAccess.UpdateInsertQuery(straddMasterlist, masterparmas);
                Task storage = SqlDataAccess.UpdateInsertQuery(straddStorage, storageparams);

                await Task.WhenAll(master, storage);
                result = true;
            }
            else
            {
                string straddStorage = "pressaddStoragelist";
                var storageparams = new
                {
                    _Partnum = obj.Partnum,
                    _Racksnum = obj.Racksnum,
                    _Levelnum = obj.Levelnum,
                    _Postnum = obj.Postnum,
                    _Boxnum = obj.Boxnum,
                    _Quantity = obj.Quantity
                };
                await SqlDataAccess.UpdateInsertQuery(straddStorage, storageparams);
                result = true;
            }

            return result;
          
        }

        public async Task<bool> InsertSummaryData(object parameters = null)
        {
            string strquery = "INSERT INTO PartslocatorPress_Received(FA_Shoporder, FA_Plan, Storage_ID, Received) " +
                              "VALUES(@FA_Shoporder, @FA_Plan, @Storage_ID, @Received)";         

            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);

            return result;
        }

        public async Task<bool> UpdateStorageData(int StorageID, int quan)
        {
            string strquery = "UPDATE PartslocatorPress_Storage SET Quantity =@Quantity " +
                              "WHERE Storage_ID = @Storage_ID";
            var parameters = new { Storage_ID = StorageID, Quantity  = quan};

            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);

            return result;
        }

        public async Task<bool> UpdateMasterlistData(object parameters, int MastID, int noteID)
        {
            string strquery = "UPDATE PartslocatorPress_Storage SET Racksnum =@Racksnum,  Quantity =@Quantity, Postnum =@Postnum " +
                              "WHERE Storage_ID = @Storage_ID";
     
            bool result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);

            if (result)
            {
                string strupdateID = "UPDATE PartslocatorPress_masterlist SET NoteID =@NoteID " +
                              "WHERE Master_ID = @Master_ID";
                var Idparams = new { Master_ID = MastID, NoteID = noteID };
                await SqlDataAccess.UpdateInsertQuery(strupdateID, Idparams);
            }

            return result;
        }

        public async Task<bool> UpdateIssuance(dynamic parameters = null)
        {
            string strquery = "INSERT INTO PartslocatorPress_Issuance(IssuanceID, IssuedQuan, IssuedBy) " +
                             "VALUES(@IssuanceID, @IssuedQuan, @IssuedBy)";

            string strupdate = "UPDATE  PartslocatorPress_Received SET Stats = 1 " +
                             "WHERE IssuanceID =@IssuanceID AND Stats = 0";

            // Run both queries in parallel using Task.WhenAll
            var insertTask = SqlDataAccess.UpdateInsertQuery(strquery, parameters);
            var updateTask = SqlDataAccess.UpdateInsertQuery(strupdate, new { parameters.IssuanceID });

            bool[] results = await Task.WhenAll(insertTask, updateTask);

            return results.All(r => r);
        }
    }
}