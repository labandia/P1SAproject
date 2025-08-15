using MSDMonitoring.Interface;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
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
            string strinsert = $@"INSERT INTO MSD_MonitorList(ReelID, AmbassadorPartnum, DateIn, InputIn, QuantityIN, Line, RemainFloor, LotNo) 
                                     VALUES(@ReelID, @AmbassadorPartnum, @DateIn, @InputIn, @QuantityIN, @Line, @RemainFloor, @LotNo)";
            return  SqlDataAccess.UpdateInsertQuery(strinsert, 
                new { 
                    ReelID = msd.ReelID,
                    AmbassadorPartnum = msd.AmbassadorPartnum,
                    DateIn = msd.DateIn,
                    InputIn = msd.InputIn,
                    QuantityIN = msd.QuantityIN, 
                    Line = msd.Line,
                    RemainFloor = msd.RemainFloor,
                    LotNo = msd.LotNo
                });
        }

        public async Task<bool> UpdateComponentsData(InputOUT_MSD msd, string ReelID, decimal totalhours)
        {
         
            string strinsert = $@"UPDATE MSD_MonitorList SET DateOut =@DateOut, INputOut =@INputOut, QuantityOut =@QuantityOut, 
                                  RemainFloor =@RemainFloor, IsStats =@IsStats
                                  WHERE RecordID =@RecordID";

            bool result = await SqlDataAccess.UpdateInsertQuery(strinsert, msd);

            if (result)
            {
                double getTotalHours = await SqlDataAccess.GetCountByDouble($@"SELECT
	                                    SUM(CAST(DATEDIFF(SECOND, e.DateIn, e.DateOut) / 3600.0 AS DECIMAL(10, 2))) AS Exphours
                                    FROM MSD_MonitorList e
                                    WHERE e.ReelID = @ReelID ", new { ReelID = ReelID });

                if (getTotalHours == 0)
                {
                    return await SqlDataAccess.UpdateInsertQuery(@"
                                UPDATE MSD_MonitorList 
                                SET TotalHours = @TotalHours
                                WHERE RecordID = @RecordID",
                                new { TotalHours = totalhours, RecordID = msd.RecordID }
                            );
                }
                else
                {
                    return await SqlDataAccess.UpdateInsertQuery(@"
                                        UPDATE MSD_MonitorList 
                                        SET TotalHours = @TotalHours
                                        WHERE RecordID = @RecordID",
                        new { TotalHours = getTotalHours, RecordID = msd.RecordID }
                    );
                }
            }
            else
            {
                return false;
            }
        }

        public Task<List<MSDmodel>> GetMSDHistoryList(int CurrentPageIndex, int pageSize, string searchTerm = "")
        {
            return SqlDataAccess.GetData<MSDmodel>(
                "MSDHistory",
                new
                {
                    PageNumber = CurrentPageIndex,
                    PageSize = pageSize,
                    SearchTerm = searchTerm
                });
        }

        public Task<int> GetTotalHistoryList()
        {
            return SqlDataAccess.GetCountData("Select COUNT(*) FROM MSD_MonitorList");
        }


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

        public Task<bool> EditComponentsData(int ID, int quan)
        {
            string strinsert = $@"UPDATE MSD_MonitorList SET  QuantityIN =@QuantityIN
                                     WHERE RecordID =@RecordID";
            return  SqlDataAccess.UpdateInsertQuery(strinsert, new { RecordID = ID, QuantityIN = quan});
        }

        public Task<List<MSDmodel>> GetMSDExportList()
        {
            return SqlDataAccess.GetData<MSDmodel>("GetExportClose");
        }

        public Task<bool> UpdateExportHistory(int ID)
        {
            string strinsert = $@"UPDATE MSD_MonitorList SET  IsExport = 1
                                     WHERE RecordID =@RecordID";
            return SqlDataAccess.UpdateInsertQuery(strinsert, new { RecordID = ID});
        }
    }
}
