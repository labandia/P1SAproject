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
        // ---------------------------
        // GET DATA DISPLAY
        // ---------------------------
        public Task<List<MSDCardModel>> GetListComponentIN()
        {
            string strquery = $@"SELECT 
	                            TOP 12
	                            RecordID, ReelID, Line, 
	                            QuantityIN, 
                                DateIn as DateCheck,
	                            FORMAT(DateIn, 'MM/dd/yy') as DateIn,
	                            FORMAT(DateIn, 'HH:mm') as TimeIn, 
	                            RemainFloor, InputIn
                            FROM MSD_MonitorList
                            WHERE IsStats = 0 AND 
                            LINE IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12)
                            ORDER BY DateIn DESC";
            return SqlDataAccess.GetData<MSDCardModel>(strquery);
        }
        public Task<List<MSDMasterlistodel>> GetMSDMasterlist() => SqlDataAccess.GetData<MSDMasterlistodel>("MSDMaster");
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
        public Task<List<MSDmodel>> GetMSDExportList() => SqlDataAccess.GetData<MSDmodel>("GetExportClose");
        public async Task<MSDReelID> GetReelID(string reelid)
        {
            var data = await SqlDataAccess.GetData<MSDReelID>($@"SELECT 
	                                                        c.ReelID, c.FloorLife, 
	                                                        m.Level,
	                                                        c.RemainQuantity, c.IsDone 
                                                        FROM  MSD_ReelIDCheck c
                                                        INNER JOIN MSD_Masterlist m ON c.AmbassadorPartnum = m.AmbassadorPartnum
                                                        WHERE c.ReelID = @ReelID",  new { ReelID = reelid });
            return data.SingleOrDefault();
        }


        public Task<int> GetTotalHistoryList() => SqlDataAccess.GetCountData("Select COUNT(*) FROM MSD_MonitorList");
       

        // ---------------------------
        // INSERT AND UPDATE DATA 
        // ---------------------------
        public Task<bool> AddComponentsData(InputIN_MSD msd)
        {
            string strinsert = $@"INSERT INTO MSD_MonitorList(ReelID, AmbassadorPartnum, DateIn, InputIn, QuantityIN, Line, RemainFloor, LotNo) 
                                     VALUES(@ReelID, @AmbassadorPartnum, @DateIn, @InputIn, @QuantityIN, @Line, @RemainFloor, @LotNo)";
            return SqlDataAccess.UpdateInsertQuery(strinsert,
                new
                {
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
                                  RemainFloor =@RemainFloor, IsStats =@IsStats, PlanQty =@PlanQty
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
        public Task<bool> UpdateExportHistory(int ID)
        {
            string strinsert = $@"UPDATE MSD_MonitorList SET  IsExport = 1
                                     WHERE RecordID =@RecordID";
            return SqlDataAccess.UpdateInsertQuery(strinsert, new { RecordID = ID});
        }

        public Task<bool> AddEditMasterlistData(MSDMasterlistodel msd, int act)
        {
            if (act == 0)
            {
                string strinsert = $@"INSERT INTO MSD_Masterlist(AmbassadorPartnum, Partname, SupplyPartName, SupplyName, Level, FloorLife) 
                                     VALUES(@AmbassadorPartnum, @Partname, @SupplyPartName, @SupplyName, @Level, @FloorLife)";
                return SqlDataAccess.UpdateInsertQuery(strinsert, msd);
            }
            else
            {
                string strinsert = $@"UPDATE MSD_Masterlist SET  Partname =@Partname, SupplyPartName =@SupplyPartName, SupplyName =@SupplyName,  Level =@Level, FloorLife =@FloorLife
                                     WHERE AmbassadorPartnum =@AmbassadorPartnum";
                return SqlDataAccess.UpdateInsertQuery(strinsert, msd);
            }

        }
    
    }
}
