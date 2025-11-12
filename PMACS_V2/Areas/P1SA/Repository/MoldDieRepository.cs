using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.P1SA.Repository
{
    public class MoldDieRepository : IDieMold
    {
        // ===========================================================
        // ==================== MOLDING DIE MOLD  ======================
        // ===========================================================

        // MOLD DIE INPUT DATA
        public Task<List<DieMoldTotalPartnum>> GetMoldTotalPartNoList(int month, int year, string process)
        {
            // filter by   WHERE p.ProcessID = 'M003' 

            string totalpartNoquery = @"WITH TotalDiePerPart AS (
                                        SELECT PartNo, SUM(TotalDie) AS TotalQty
                                        FROM DieMoldMonitor
                                        GROUP BY PartNo
                                    ),
                                    SingleDS AS (
                                        SELECT *
                                        FROM (
                                            SELECT PartNo, TotalDie, DateAction,
                                                   ROW_NUMBER() OVER (PARTITION BY PartNo ORDER BY DateAction DESC) AS rn
                                            FROM DieMoldMonitor
                                            WHERE MONTH(DateAction) = @Month
                                            AND YEAR(DateAction) = @Year
                                        ) AS RankedDS
                                        WHERE rn = 1
                                    )
                                     SELECT 
                                        p.RecordID,
										p.No,
										p.PartNo,
										md.Description,
										mp.DimensionQuality,
										p.DieNumber,
										p.DieSerial,
										mp.Cavity,
										ds.DateAction,
                                        ds.TotalDie,
                                        td.TotalQty
                                    FROM DieMoldProcesses p
									INNER JOIN DieMoldParts mp ON mp.PartNo = p.PartNo
									INNER JOIN DieMoldDescription md ON md.PartDescriptionID = mp.PartDescriptionID
                                    LEFT JOIN SingleDS ds ON ds.PartNo = p.PartNo
                                    LEFT JOIN TotalDiePerPart td ON td.PartNo = p.PartNo
                                    WHERE p.ProcessID = @ProcessID
                                    ORDER BY p.No ASC;";

            var parameters = new { Month = month, Year = year, ProcessID = process };

            return  SqlDataAccess.GetData<DieMoldTotalPartnum>(totalpartNoquery, parameters);
        }
        public async Task<List<DieMoldTotalPartnum>> GetMoldDieMonthInput(int month, int year, string process = "M002")
        {
            var totalpart = await GetMoldTotalPartNoList(month, year, process);
            // Step 1: Group by No and calculate total for each No
            var totalByNo = totalpart
                .GroupBy(x => x.No)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.TotalQty));

            // Step 2: Update each item's TotalQty to the total of its group
            foreach (var item in totalpart)
            {
                item.TotalNo = totalByNo[item.No];
            }

     
            return totalpart;
        }

        // MOLD DIE SUMMARY DATA
        public Task<List<DieMoldSetNotal>> GetSummaryMoldData()
        {
            string strquery = @"SELECT No, PreviousCount, DieLife
                              FROM DieMoldProcesses
                              GROUP BY No,  PreviousCount, DieLife";
            return SqlDataAccess.GetData<DieMoldSetNotal>(strquery, null);
        }
        public Task<List<DieMoldDaily>> GetDailyMoldData()
        {
            return SqlDataAccess.GetData<DieMoldDaily>("SELECT * FROM DieMoldDaily");
        }
        public Task<List<DieMoldProcess>> GetMoldProcess()
        {
            return SqlDataAccess.GetData<DieMoldProcess>("SELECT * FROM DieMoldProcessName WHERE ProcessID IN ('M001', 'M002', 'M003', 'M004', 'M005')");
        }

        public Task<List<DieProcessDescription>> GetMoldPartDescription()
        {
            return SqlDataAccess.GetData<DieProcessDescription>("SELECT * FROM DieMoldDescription");
        }

        public Task<List<DieMoldSummaryProcess>> GetMoldDieSummary(string process)
        {
            // filter by   WHERE p.ProcessID = 'M003' 

            // Get the TotalQty of the Mold die Input
            string strquery = @" WITH TotalDiePerPart AS (
                                    SELECT PartNo, SUM(TotalDie) AS TotalQty
                                    FROM DieMoldMonitor
                                    GROUP BY PartNo
                                ),
                                SingleDS AS (
                                    SELECT PartNo, TotalDie, DateAction
                                    FROM (
                                        SELECT PartNo, TotalDie, DateAction,
                                               ROW_NUMBER() OVER (PARTITION BY PartNo ORDER BY DateAction DESC) AS rn
                                        FROM DieMoldMonitor
                                    ) Ranked
                                    WHERE rn = 1
                                ),
                                TotalByNo AS (
                                    SELECT p.No, SUM(ISNULL(td.TotalQty, 0)) AS ShotOnwards
                                    FROM DieMoldProcesses p
                                    LEFT JOIN TotalDiePerPart td ON td.PartNo = p.PartNo
                                    GROUP BY p.No
                                )
                                SELECT 
                                    p.RecordID,
                                    p.No,
                                    p.ProcessID,
                                    p.PartNo,
                                    p.DieNumber,
                                    p.DieSerial,
                                    p.DieLife,
                                    p.PreviousCount,
                                    ISNULL(mp.Cavity, 0) AS Cavity,
                                    ISNULL(mp.DimensionQuality, '') AS DimensionQuality,
                                    ISNULL(md.Description, '') AS Description,
                                    ISNULL(ds.DateAction, NULL) AS DateAction,
                                    ISNULL(ds.TotalDie, 0) AS LatestTotalDie,
                                    ISNULL(tb.ShotOnwards, 0) AS ShotOnwards,
                                    ISNULL(p.PreviousCount, 0) + ISNULL(tb.ShotOnwards, 0) AS TotalShotCount,
                                    CASE 
                                        WHEN ISNULL(p.DieLife, 0) = 0 THEN 0
                                        ELSE ((ISNULL(p.PreviousCount, 0) + ISNULL(tb.ShotOnwards, 0)) * 100) / p.DieLife
                                    END AS Status,
                                    CASE 
                                        WHEN ((ISNULL(p.PreviousCount, 0) + ISNULL(tb.ShotOnwards, 0)) * 100) / NULLIF(p.DieLife, 0) >= 100 THEN 'End of Life'
                                        ELSE 'For Monitoring'
                                    END AS Remarks
                                FROM DieMoldProcesses p
                                LEFT JOIN DieMoldParts mp ON mp.PartNo = p.PartNo
                                LEFT JOIN DieMoldDescription md ON md.PartDescriptionID = mp.PartDescriptionID
                                LEFT JOIN SingleDS ds ON ds.PartNo = p.PartNo
                                LEFT JOIN TotalDiePerPart td ON td.PartNo = p.PartNo
                                LEFT JOIN TotalByNo tb ON tb.No = p.No
                                WHERE p.ProcessID = @ProcessID
                                ORDER BY p.No ASC";

            return  SqlDataAccess.GetData<DieMoldSummaryProcess>(strquery, new { ProcessID = process });
        }
        public Task<List<DieMoldToolingModelDisplay>> GetMoldToolingData()
        {
            string strquery = $@"SELECT t.RecordID, t.RegNo, t.PartNo, p.DimensionQuality, 
                                t.Item, t.DetailsModify, t.ShotRelease,
                                FORMAT(t.DateArrived, 'MM/dd/yy') as DateArrived,
                                FORMAT(t.DateRepair, 'MM/dd/yy') as DateRepair,
                                t.Incharge, t.Remarks
                                FROM DieMoldDieTooling t
                                INNER JOIN DieMoldParts p ON t.PartNo = p.PartNo
                                ORDER BY t.RecordID DESC";

            return SqlDataAccess.GetData<DieMoldToolingModelDisplay>(strquery, null, "MoldTooling");
        }

        public async Task<bool> AddUpdateMoldie(MoldInputModel mold)
        {
            string checkquery = "SELECT COUNT(PartNo) FROM DieMoldMonitor WHERE MONTH(DateAction) = @DateAction AND PartNo = @PartNo";
            bool IsExist = await SqlDataAccess.Checkdata(checkquery, new { DateAction = mold.DateAction, PartNo = mold.PartNo });

            // If Exist Updates the Data Only
            if (IsExist)
            {
                string updatequery = @"UPDATE DieMoldMonitor SET TotalDie =@TotalDie WHERE MONTH(DateAction) = @DateAction AND PartNo = @PartNo";
                var parameter = new { TotalDie = mold.MoldInput, DateAction = mold.DateAction, PartNo = mold.PartNo };
                return await SqlDataAccess.UpdateInsertQuery(updatequery, parameter);
            }
            else
            {
                //INSERT NEW DATA
                DateTime date = new DateTime(2025, mold.DateAction, 1);
                string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                string insertquery = @"INSERT INTO DieMoldMonitor(PartNo, DateAction, TotalDie)
                                       VALUES(@PartNo, @DateAction, @TotalDie)";
                var parameter = new { PartNo = mold.PartNo, DateAction = formattedDate, TotalDie = mold.MoldInput };
                return await SqlDataAccess.UpdateInsertQuery(insertquery, parameter);
            }
          
        }

        public Task<bool> AddMoldieTooling(DieMoldToolingModel mold)
        {
            string insertquery = @"INSERT INTO DieMoldDieTooling(RegNo, PartNo, Item, Item2, DetailsModify, DetailsModify2, ShotRelease, 
                                                                DateArrived, DateRepair, Incharge, Remarks)
                                   VALUES(@RegNo, @PartNo, @Item, @Item2, @DetailsModify, @DetailsModify2, @ShotRelease, @DateArrived , @DateRepair, 
                                    @Incharge, @Remarks)";
            var parameter = new
            {
                RegNo = mold.RegNo,
                PartNo = mold.ParNoSearch,
                Item = mold.Item,
                Item2 = mold.Item2,
                DetailsModify = mold.DetailsModify,
                DetailsModify2 = mold.DetailsModify2,
                ShotRelease = mold.ShotRelease, 
                DateArrived = mold.DateArrived,
                DateRepair = mold.DateRepair,   
                Incharge = mold.Incharge,   
                Remarks = mold.Remarks
            };
            return SqlDataAccess.UpdateInsertQuery(insertquery, parameter, "MoldTooling");
        }


        public async Task<bool> AddNewMoldDie(AddDieMoldingDataInput mold)
        {

            // 1. Check and Insert A new Partnumber
            string partinsertquery = $@"INSERT INTO DieMoldParts(PartNo, PartDescriptionID, DimensionQuality, Cavity)
                                        SELECT @PartNo, @PartDescriptionID, @DimensionQuality, @Cavity
                                        WHERE NOT EXISTS(
	                                        SELECT 1 FROM DieMoldParts WHERE PartNo =@PartNo
                                        );";
            var insertparams = new
            {
                PartNo = mold.AddPartno,
                PartDescriptionID = mold.PartDescriptionID,
                DimensionQuality = mold.DimensionQual,
                Cavity = mold.AddCavity
            };

            // 2. Checks and Insert to a new Process and partnumber
            string processinsertquery = $@"INSERT INTO DieMoldProcesses(No, ProcessID, PartNo, DieNumber, DieSerial)
                                        SELECT @No, @ProcessID, @PartNo, @DieNumber, @DieSerial
                                        WHERE NOT EXISTS(
	                                        SELECT 1 FROM DieMoldProcesses WHERE (No =@No AND PartNo =@PartNo) AND ProcessID =@ProcessID
                                        );";


            var processparams = new
            {
                No = mold.AddNo,
                ProcessID = mold.AddSelectProcess,
                PartNo = mold.AddPartno,
                DieNumber = mold.AddDienum,
                DieSerial = mold.AddSerial
            };


            //bool result = await SqlDataAccess.UpdateInsertQuery(partinsertquery, insertparams);

            var addpart = SqlDataAccess.UpdateInsertQuery(partinsertquery, insertparams);
            var addprocess = SqlDataAccess.UpdateInsertQuery(processinsertquery, processparams);

            bool[] results = await Task.WhenAll(addpart, addprocess);

            return results.All(r => r);
 
        }
        public async Task<bool> UpdateMoldDie(DieMoldingDataInput mold)
        {
            string updateprocessQuery = $@"UPDATE DieMoldProcesses SET  DieNumber =@DieNumber,  DieSerial =@DieSerial
                                       WHERE RecordID =@RecordID";
            var parameter = new { DieNumber = mold.EditDie, DieSerial = mold.EditSerial, RecordID = mold.RecordID };


            string updatepartsQuery = $@"UPDATE DieMoldParts SET  Cavity =@Cavity
                                       WHERE PartNo =@PartNo";

            // Run both queries in parallel
            var updateProcess = SqlDataAccess.UpdateInsertQuery(updateprocessQuery, parameter);
            var updateParts =  SqlDataAccess.UpdateInsertQuery(updatepartsQuery, new { Cavity = mold.EditCavity, PartNo  = mold.EditPartNo});

            bool[] results = await Task.WhenAll(updateProcess, updateParts);

            return results.All(r => r);
        }
        public async Task<bool> DeleteMoldDie(int ID, string partnum = "")
        {
            var delParts = SqlDataAccess.UpdateInsertQuery("DELETE FROM DieMoldParts WHERE PartNo =@PartNo", new { PartNo = partnum });
            var delProcess = SqlDataAccess.UpdateInsertQuery("DELETE FROM DieMoldProcesses WHERE RecordID =@RecordID", new { RecordID = ID });

            bool[] results = await Task.WhenAll(delParts, delProcess);

            return results.All(r => r);
        }


        // ===========================================================
        // ==================== PRESS DIE MOLD  ======================
        // ===========================================================
        public  Task<List<PressDieRegistry>> GetPressRegistryList()
        {
            string strquery = @"SELECT ToolNo,Type,Model,Lines,Note,Status,Operational
                                FROM DiePressRegistry";
            return SqlDataAccess.GetData<PressDieRegistry>(strquery, null);
        }

        public Task<List<PressMainMonitor>> GetPressMainMonitoring()
        {
            string strquery = $@"SELECT m.MonitorID, m.ToolNo, r.Type, m.Line, 
                                  m.MinUpper, m.MinLower, m.TotalPressStamp, m.Up, m.low,
                                  CASE 
                                        WHEN r.Operational = 0 THEN 'End of Life'
                                        WHEN r.Operational = 1 THEN 'Monitoring'
                                        ELSE 'Unknown'
                                    END AS Operational
                                  FROM DiePressMainMonitor m
                                  INNER JOIN DiePressRegistry r ON r.ToolNo = m.ToolNo ";
            return SqlDataAccess.GetData<PressMainMonitor>(strquery, null);
        }


        public Task<List<PressDieMontoring>> GetPressMonitoring()
        {
            string strquery = @" SELECT p.MonitorID, p.RecordID,
                                  FORMAT(p.DateInput, 'MM/dd/yy') as DateInput, p.ToolNo,
                                  (m.Up -  ABS(p.Upper_ActualHeight - (SELECT TOP 1 Upper_DrawingHeight
                                FROM DiePressMonitoring
                                ORDER BY RecordID ASC))) as Up,
                                  (m.low - ABS(p.Lower_ActualHeight - (SELECT TOP 1 Lower_DrawingHeight
                                FROM DiePressMonitoring
                                ORDER BY RecordID ASC))) as Low,
                                  p.Upper_ActualHeight, p.Upper_DrawingHeight, 
                                    ABS(p.Upper_ActualHeight - (SELECT TOP 1 Upper_DrawingHeight
                                FROM DiePressMonitoring
                                ORDER BY RecordID ASC)) as GrindUpper,
                                  p.Lower_ActualHeight, p.Lower_DrawingHeight, 
                                   ABS(p.Lower_ActualHeight - (SELECT TOP 1 Lower_DrawingHeight
                                FROM DiePressMonitoring
                                ORDER BY RecordID ASC)) as GrindLower, 
                                  p.PressStamp, p.DieStatus
                                  FROM DiePressMonitoring p
                                  INNER JOIN DiePressMainMonitor m ON p.MonitorID = m.MonitorID";
            return SqlDataAccess.GetData<PressDieMontoring>(strquery, null);
        }
        public async Task<List<PressDieSummary>> GetPressSummary()
        {
            var data = await PressDieSummaryList();
            var diepart = await PressDieUpperLowerlist();
            double minValue = 0.0;

            if (data != null && data.Any())
            {
                foreach (var item in data)
                {
                    //int minValue = Math.Min(item, b);
                    double? dieUpper = diepart.Where(d => d.ToolNo == item.ToolNo)
                            .Select(d => d.UpperDieHeight).FirstOrDefault();
                    double? lowerUpper = diepart.Where(d => d.ToolNo == item.ToolNo)
                            .Select(d => d.LowerDieHeight).FirstOrDefault();

                    if (dieUpper.HasValue && lowerUpper.HasValue)
                    {
                        minValue = Math.Min(dieUpper.Value, lowerUpper.Value);
                        // Do something with minValue
                    }

                    item.Status = GetStatusMonitor(minValue);

                    //Debug.WriteLine($@"ToolNo : {item.ToolNo} = MIn : {item.Status}");
                }
            }

            return data;    
        }

        public Task<List<PressDieLowerUpper>> PressDieUpperLowerlist()
        {
            string strquery = @"SELECT 
                                ToolNo,
                                MAX(CASE WHEN DiePart = 'Upper' THEN DieHeight END) AS UpperDieHeight,
                                MAX(CASE WHEN DiePart = 'Lower' THEN DieHeight END) AS LowerDieHeight
                            FROM DiePressSummary
                            GROUP BY ToolNo
                            ORDER BY ToolNo";
            return SqlDataAccess.GetData<PressDieLowerUpper>(strquery, null);
        }
        public Task<List<PressDieSummary>> PressDieSummaryList()
        {
            string strquery = @"WITH TotalPressStamp AS (
                                    SELECT ToolNo, TotalPressStamp
                                    FROM DiePressMainMonitor
                                )
                                SELECT  
                                s.ToolNo, 
                                r.Type, r.Model, 
                                s.DiePart, s.DieHeight, 
                                s.StdGrind, s.StampGrind, 
                                s.Line, s.Avg, 
                                (s.DieHeight / s.StdGrind * s.StampGrind * s.Line / s.Avg) as Remaining, 
                                td.TotalPressStamp
                                FROM DiePressSummary s 
                                INNER JOIN  DiePressRegistry r ON r.ToolNo = s.ToolNo
                                LEFT JOIN TotalPressStamp td ON td.ToolNo = s.ToolNo";
            return  SqlDataAccess.GetData<PressDieSummary>(strquery, null);
        }
        public Task<List<PressDieControlModel>> GetPressControl()
        {
            string strsql = $@"SELECT FORMAT(c.DateInput, 'MM/dd/yy') as DateInput,
                                c.ToolNo, c.Brand, r.Type, c.Stamp, c.Machine, 
	                            c.DieCondition, c.Operator, c.DieHeight, c.LeaderCom, 
	                            c.Gear, c.Pitch, c.GearCom, c.ReasonDetach
                            FROM DiePressControl c INNER JOIN DiePressRegistry r 
                            ON c.ToolNo = r.ToolNo";
            return SqlDataAccess.GetData<PressDieControlModel>(strsql, null, "dieControl");
        }
        public string GetStatusMonitor(double minval)
        {
            string val;

            if (minval == 12 || minval == 9.8)
            {
                val = "Max Die Life";
            }
            else if (minval == 4.9)
            {
                val = "Reach half die limit";
            }
            else if (minval == 3)
            {
                val = "For monitoring";
            }
            else if (minval > 9.8 && minval < 12)
            {
                val = "Max Die Life";
            }
            else if (minval > 4.9 && minval < 9.8)
            {
                val = "Max Die Life";
            }
            else if (minval > 3 && minval < 4.9)
            {
                val = "Reach half die limit";
            }
            else
            {
                val = "End of Life";
            }

            return val;
        }

        public Task<bool> AddUpdatePressMonitoring(PressInputModel press)
        {
            string insertquery = @"INSERT INTO DiePressMainMonitor(ToolNo, Up, low, Line)
                                       VALUES(@ToolNo, @Up, @low, @Line)";
            return  SqlDataAccess.UpdateInsertQuery(insertquery, press);
        }

        public async Task<bool> AddPressMonitorData(PressMonitorInput press)
        {
            string insertquery = @"INSERT INTO DiePressMonitoring(MonitorID, ToolNo, Upper_ActualHeight, 
                                 Upper_DrawingHeight, Lower_ActualHeight, Lower_DrawingHeight, PressStamp)
                                   VALUES(@MonitorID, @ToolNo, @Upper_ActualHeight, @Upper_DrawingHeight, 
                                    @Lower_ActualHeight, @Lower_DrawingHeight, @PressStamp)";
            bool insertResult = await SqlDataAccess.UpdateInsertQuery(insertquery, press);

            if (insertResult)
            {
                int getTotal = await SqlDataAccess.GetCountData($@"SELECT SUM(PressStamp) as Total
                                                    FROM DiePressMonitoring
                                                    WHERE MonitorID =@MonitorID
                                                    GROUP BY ToolNo", new { MonitorID = press.MonitorID });

                string updateQuery = @"UPDATE DiePressMainMonitor SET TotalPressStamp =@TotalPressStamp WHERE MonitorID =@MonitorID";

                await SqlDataAccess.UpdateInsertQuery(updateQuery, new { TotalPressStamp = getTotal, MonitorID = press.MonitorID });
            }

            return insertResult;
        }
        public Task<bool> UpdateEndofLifeMonitorData(string ToolNo)
        {
            string updatequery = $@"UPDATE DiePressRegistry SET Operational = 0
                                    WHERE ToolNo =@ToolNo";
            return SqlDataAccess.UpdateInsertQuery(updatequery, new {ToolNo = ToolNo});
        }
        public Task<bool> EditPressRegistry(PressDieRegistry press)
        {
            string updatequery = $@"UPDATE DiePressRegistry SET Type =@Type, Model =@Model, Lines =@Lines, Status =@Status,  Operational =@Operational
                                    WHERE ToolNo =@ToolNo";

            return SqlDataAccess.UpdateInsertQuery(updatequery, press);
        }

        public async Task<bool> AddPressRegistry(PressDieRegistry press)
        {
            var data = await GetPressRegistryList();
            bool check = data.Any(res =>  res.ToolNo == press.ToolNo);

            if (check)
            {
                Debug.WriteLine("Already Exist");
                return false;
            }

            string insertquery = @"INSERT INTO DiePressRegistry(ToolNo, Type, Model, Lines, Status, Operational)
                                       VALUES(@ToolNo, @Type, @Model, @Lines, @Status, @Operational)";
            return await SqlDataAccess.UpdateInsertQuery(insertquery, press);

        }

        public Task<bool> AddPressDieControl(PressDieControlData press)
        {
            string insertquery = @"INSERT INTO DiePressControl(ToolNo, Stamp, Machine, DieCondition, DieHeight, Operator, LeaderCom, Gear, Pitch)
                                       VALUES(@ToolNo, @Stamp, @Machine, @DieCondition, @DieHeight, @Operator, @LeaderCom, @Gear, @Pitch)";
            return SqlDataAccess.UpdateInsertQuery(insertquery, press);
        }

        public Task<bool> AddDailyMoldie(DieMoldDaily mold)
        {
            string insertquery = @"INSERT INTO DieMoldDaily(PartNo, DateInput, TotalDie, CycleShot, Total, MachineNo, Remarks, Mincharge)
                                       VALUES(@PartNo, @DateInput, @TotalDie, @CycleShot, @Total, @MachineNo, @Remarks, @Mincharge)";
            return SqlDataAccess.UpdateInsertQuery(insertquery, mold);
        }

       
    }
}