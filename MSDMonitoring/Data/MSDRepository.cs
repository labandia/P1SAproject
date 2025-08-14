using MSDMonitoring.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSDMonitoring.Data
{
    internal class MSDRepository : IMSD
    {
        public Task<List<MSDCardModel>> GetListComponentIN()
        {
            string strquery = $@"SELECT 
	                            TOP 12
	                            RecordID, ReelID, Line, 
	                            QuantityIN, 
                                DateIn as DateCheck,
	                            FORMAT(DateIn, 'MM/dd/yy') as DateIn,
	                            FORMAT(DateIn, 'HH:mm') as TimeIn, 
	                            RemainFloor
                            FROM MSD_MonitorList
                            WHERE IsStats = 0 AND 
                            LINE IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)
                            ORDER BY DateIn DESC";
            return SqlDataAccess.GetData<MSDCardModel>(strquery);
        }

        public Task<bool> AddComponentsData(InputIN_MSD msd)
        {
            string strinsert = $@"INSERT INTO MSD_MonitorList(ReelID, AmbassadorPartnum, DateIn, InputIn, QuantityIN, Line, RemainFloor) 
                                     VALUES(@ReelID, @AmbassadorPartnum, @DateIn, @InputIn, @QuantityIN, @Line, @RemainFloor)";
            return  SqlDataAccess.UpdateInsertQuery(strinsert, 
                new { 
                    ReelID = msd.ReelID,
                    AmbassadorPartnum = msd.AmbassadorPartnum,
                    DateIn = msd.DateIn,
                    InputIn = msd.InputIn,
                    QuantityIN = msd.QuantityIN, 
                    Line = msd.Line,
                    RemainFloor = msd.RemainFloor
                });
        }

        public Task<bool> UpdateComponentsData(InputOUT_MSD msd)
        {
            string strinsert = $@"UPDATE MSD_MonitorList SET DateOut =@DateOut, INputOut =@INputOut, QuantityOut =@QuantityOut, 
                                  RemainFloor =@RemainFloor, IsStats =@IsStats
                                  WHERE RecordID =@RecordID";
            return SqlDataAccess.UpdateInsertQuery(strinsert, msd);
        }

        public Task<List<MSDmodel>> GetMSDHistoryList() => SqlDataAccess.GetData<MSDmodel>("MSDHistory");
        public Task<List<MSDMasterlistodel>> GetMSDMasterlist() => SqlDataAccess.GetData<MSDMasterlistodel>("MSDMaster");
    

        public async Task<MSDReelID> GetReelID(string reelid)
        {
            var data = await SqlDataAccess.GetData<MSDReelID>($"SELECT ReelID, FloorLife, RemainQuantity, IsDone FROM  MSD_ReelIDCheck WHERE ReelID =@ReelID", 
                            new { ReelID = reelid });
            return data.SingleOrDefault();
        }

        

        public async Task<bool> UpdateChecker(string ReelID, double Floorlife, int Remain)
        {
            bool IsExist = await SqlDataAccess.Checkdata("SELECT TOP 1 COUNT(*) FROM MSD_ReelIDCheck WHERE ReelID =@ReelID", new { ReelID  = ReelID });

            if (IsExist)
            {
                // Update the Data 
                string strinsert = $@"UPDATE MSD_ReelIDCheck SET  FloorLife =@FloorLife, RemainQuantity =@RemainQuantity 
                                     WHERE ReelID =@ReelID";
                return await SqlDataAccess.UpdateInsertQuery(strinsert, new { ReelID = ReelID, FloorLife = Floorlife, RemainQuantity = Remain });
            }
            else
            {
                // INSERT A new ReelID 
                string strinsert = $@"INSERT INTO MSD_ReelIDCheck(ReelID, FloorLife, RemainQuantity) 
                                     VALUES(@ReelID, @FloorLife, @RemainQuantity)";
                return await SqlDataAccess.UpdateInsertQuery(strinsert, new { ReelID = ReelID, FloorLife = Floorlife, RemainQuantity = Remain });
            }
        }

        public Task<bool> AddHistoryData(InputMSD msd) => SqlDataAccess.UpdateInsertQuery("InsertMSD", msd);
       
    }
}
