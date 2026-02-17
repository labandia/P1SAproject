using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using PMACS_V2.Areas.MoldDie.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using PMACS_V2.Models;

namespace PMACS_V2.Areas.MoldDie.Repository
{
    public class MoldDieServices : IMoldDieModel
    {
        // ===========================================================
        // FOR DROPDOWN AND FILTERS
        // ===========================================================
        public Task<List<string>> GetMoldDieYear()
        {
             return SqlDataAccess.GetData<string>(@"SELECT 
                                ISNULL(YEAR(DateAction), YEAR(GETDATE())) AS ActionYear
                            FROM DieMoldMonitor
                            GROUP BY YEAR(DateAction) ORDER BY ActionYear DESC;");
        }

        public Task<List<string>> GetMoldDieDescription()
        {
            return SqlDataAccess.GetData<string>(@"SELECT PartDescription
                          FROM DieMold_MoldingMainParts
                          GROUP BY PartDescription;");
        }
        // ===========================================================
        // MOLD DIE OVER ALL SUMMARY AND MONTHLY
        // ===========================================================
        public Task<List<DieMoldMonitoringModel>> GetMoldDieSummary(int Month, int year, string process = "")
        {
            string filter = process != "" ? $@" WHERE ProcessID = '{process}' AND p.DieSerial != '' " : "";

            return SqlDataAccess.GetData<DieMoldMonitoringModel>($@"WITH TotalDiePerPart AS (
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
                                WHERE MONTH(DateAction) = @Month
                                AND YEAR(DateAction) = @Year
                            ) Ranked
                            WHERE rn = 1
                        ),
                        TotalByNo AS (
                            SELECT p.DieSerial, SUM(ISNULL(td.TotalQty, 0)) AS ShotOnwards
                            FROM DieMold_MoldingMainParts p
                            LEFT JOIN TotalDiePerPart td ON td.PartNo = p.PartNo
                            GROUP BY p.DieSerial
                        )
                        SELECT 
                            p.ProcessID,
                            p.PartNo,
                            p.DieNumber,
                            p.DieSerial,
                            p.PreviousCount,
                            ISNULL(p.Dimension_Quality, '') AS Dimension_Quality,
                            p.PartDescription,
                            td.TotalQty,
                            ISNULL(ds.DateAction, NULL) AS DateAction,
                            ISNULL(ds.TotalDie, 0) AS LatestTotalDie,
                            ISNULL(tb.ShotOnwards, 0) AS ShotOnwards,
                            ISNULL(p.PreviousCount, 0) + ISNULL(tb.ShotOnwards, 0) AS TotalShotCount,
                            CASE 
		                        WHEN ISNULL(1000000, 0) = 0 THEN 0
		                        ELSE CAST((ISNULL(p.PreviousCount, 0) + ISNULL(tb.ShotOnwards, 0)) AS BIGINT) * 100 / 1000000
	                        END AS Status,
	                        CASE 
		                        WHEN CAST((ISNULL(p.PreviousCount, 0) + ISNULL(tb.ShotOnwards, 0)) AS BIGINT) * 100 / 1000000 >= 100 THEN 'End of Life'
		                        ELSE 'For Monitoring'
	                        END AS Remarks
                        FROM DieMold_MoldingMainParts p
                        LEFT JOIN SingleDS ds ON ds.PartNo = p.PartNo
                        LEFT JOIN TotalDiePerPart td ON td.PartNo = p.PartNo
                        LEFT JOIN TotalByNo tb ON tb.DieSerial = p.DieSerial
                        {filter}
                        ORDER BY p.DieSerial ASC", new { month = Month, year = year });
        }
        public async Task<List<DieMoldMonitoringModel>> GetMoldDieMonthly(int Month, int year, string process = "")
        {
            var totalpart = await GetMoldDieSummary(Month, year, process);
            // Step 1: Group by No and calculate total for each No
            var totalByNo = totalpart
                .GroupBy(x => x.DieSerial)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.TotalQty));

            // Step 2: Update each item's TotalQty to the total of its group
            foreach (var item in totalpart)
            {
                item.TotalNo = totalByNo[item.DieSerial];
            }


            return totalpart;
        }
        // ===========================================================
        // ===========================================================
        // MOLD DIE DAILY FUNCTIONALITY
        public Task<List<DieMoldMonitoringModel>> GetDailyMoldData(int month, int days, int year, string process)
        {
            string filterdays = (days != 0) ? $@"AND DAY(d.DateInput) = {days}" : "";

            return SqlDataAccess.GetData<DieMoldMonitoringModel>($@"SELECT 
                            d.RecordID,
	                        FORMAT(d.DateInput, 'MM/dd/yy') as DateInput, 
	                        d.PartNo, p.Dimension_Quality, 
	                        d.CycleShot, 
	                        d.Total, 
	                        d.MachineNo, 
	                        d.Status, 
	                        d.Remarks, 
	                        d.Mincharge
                        FROM DieMold_Daily d 
                        INNER JOIN DieMold_MoldingMainParts p ON d.PartNo = p.PartNo
                       WHERE 
                            MONTH(d.DateInput) = @month
					        AND YEAR(d.DateInput) = @year
					        {filterdays}      
					        AND p.ProcessID = @process 
                        ORDER BY d.RecordID DESC", new { process = process, month = month, year = year });
        }
        public Task<List<DieMoldMonitoringModel>> GetDailyMoldHistoryData(
            string searchValue,
            string processID)
        {
            bool isPartNo = !string.IsNullOrWhiteSpace(searchValue)
                            && searchValue.StartsWith("0");


            string sql = isPartNo
                ? @"SELECT 
                d.RecordID,
                FORMAT(d.DateInput, 'MM/dd/yy') AS DateInput, 
                d.PartNo,
                p.Dimension_Quality, 
                p.DieSerial,
                d.CycleShot, 
                d.Total, 
                d.MachineNo, 
                d.Status, 
                d.Remarks, 
                d.Mincharge
            FROM DieMold_Daily d 
            INNER JOIN DieMold_MoldingMainParts p 
                ON d.PartNo = p.PartNo
            WHERE 
                d.PartNo = @SearchValue
                AND p.ProcessID = @ProcessID
            ORDER BY d.RecordID DESC;"
                : @"SELECT 
                d.RecordID,
                FORMAT(d.DateInput, 'MM/dd/yy') AS DateInput, 
                d.PartNo,
                p.Dimension_Quality, 
                p.DieSerial,
                d.CycleShot, 
                d.Total, 
                d.MachineNo, 
                d.Status, 
                d.Remarks, 
                d.Mincharge
            FROM DieMold_Daily d 
            INNER JOIN DieMold_MoldingMainParts p 
                ON d.PartNo = p.PartNo
            WHERE 
                p.DieSerial = @SearchValue
                AND p.ProcessID = @ProcessID
            ORDER BY d.RecordID DESC;";

            return SqlDataAccess.GetData<DieMoldMonitoringModel>(
                sql,
                new
                {
                    SearchValue = searchValue?.Trim(),
                    ProcessID = processID
                });
        }

        public Task<List<DieMoldMonitoringModel>> GetDailyMoldHistoryByDieSerialData(string diepart, string processID)
        {
            string strsql = $@"SELECT 
                    FORMAT(d.DateInput, 'MM/dd/yy') AS DateInput,             
				    p.DieSerial,
                    d.CycleShot, 
                    d.Total, 
                    d.MachineNo, 
                    d.Status, 
                    d.Remarks, 
                    d.Mincharge
                FROM DieMold_Daily d 
                INNER JOIN DieMold_MoldingMainParts p 
                    ON d.PartNo = p.PartNo
                WHERE 
                    p.DieSerial = @SearchValue
                    AND p.ProcessID = @ProcessID
			    GROUP BY p.DieSerial, d.DateInput, d.CycleShot,
			    d.Total, d.MachineNo, d.Status, d.Remarks, d.Mincharge
                 ORDER BY d.DateInput DESC;";

            return SqlDataAccess.GetData<DieMoldMonitoringModel>(
              strsql,
              new
              {
                  SearchValue = diepart,
                  ProcessID = processID
              });
        }

 

        public async Task<bool> AddUpdateDailyMoldie(DieMoldMonitoringModel mold, int action)
        {
             if (action == 0)
            {
                var dateOnly = mold.DateInput;
                // ===========================================================
                // CASE 1: Part number already exists → compute new total
                // ===========================================================

                bool partExists = await SqlDataAccess.Checkdata(
                           "SELECT 1 FROM DieMold_Daily WHERE PartNo = @PartNo",
                           new { PartNo = mold.PartNo });

                // Get last total shot
                int lasttotal = partExists ? await SqlDataAccess.GetCountData($@"
                    SELECT TOP 1 Total
                    FROM DieMold_Daily
                    WHERE PartNo = @PartNo 
                    ORDER BY RecordID DESC",
                    new { PartNo = mold.PartNo }) : 0;

                Debug.WriteLine("Done Getting the Last Cycle shot  ... ");
                Debug.WriteLine(mold.PartNo + " --- " + mold.ProcessID);

                var history = await GetMoldieMasterlistParts(mold.PartNo);

                //if (history == null) return false;
                Debug.WriteLine("Done Getting the Last Record History  ... ");
                Debug.WriteLine(history.Dimension_Quality);

                string serial = mold.DieSerial;

                var (moldtotal, moldstatus) = CalculateTotalAndStatus(
                    lasttotal,
                    mold.CycleShot,
                    mold.ProcessID,
                    history.Dimension_Quality);
                Debug.WriteLine("Done Calculating Total and Status ... ");

                // =====================================================
                // DAILY MONITORING 
                // =====================================================
                await UpsertDailyAsync(mold.PartNo, dateOnly, mold.CycleShot, moldtotal, moldstatus, mold);

                Debug.WriteLine("Done Updating Daily ... ");

                // =====================================================
                // INSERT MONITOR LOG
                // =====================================================

                await UpsertMonitor(mold.PartNo, dateOnly, mold.CycleShot);

                Debug.WriteLine("Done Updating Monitoring ... ");

                // =====================================================
                // 🔥 UPDATE OTHER PART NUMBERS WITH SAME DIE SERIAL
                // =====================================================

                var getpartnumbers = await GetSamePartsNo(mold.DieSerial, mold.ProcessID, mold.PartNo);

                Debug.WriteLine("Done Retriving  Partnumbers ... ");

                foreach (var part in getpartnumbers)
                {
                    await UpsertDailyAsync(
                        part,
                        dateOnly,
                        mold.CycleShot,
                        moldtotal,
                        moldstatus,
                        mold);
                }
                Debug.WriteLine("Done Proess Completed !!!!!!!!! ... ");
                return true;
        
            }
            else
            {
                // Get all related PartNo using DieSerial
                var partList = await GetPartNoByDieSerial(mold.DieSerial);
                if (!partList.Any()) return false;


                foreach (var part in partList)
                {
                    // Get last total BEFORE this date
                    string getLastTotal = @"
                                SELECT TOP 1 ISNULL(Total, 0)
                                FROM DieMold_Daily
                                WHERE PartNo = @PartNo
                                  AND DateInput < @DateInput
                                ORDER BY DateInput DESC, RecordID DESC";

                    int lastTotal = await SqlDataAccess.GetCountData(getLastTotal, new
                    {
                        PartNo = part,
                        DateInput = mold.DateInput
                    });

                    int newTotal = lastTotal + mold.CycleShot;

                    // Update DAILY
                    string updateDaily = @"
                            UPDATE DieMold_Daily
                            SET
                                CycleShot = @CycleShot,
                                Total = @Total,
                                MachineNo = @MachineNo,
                                Remarks = @Remarks,
                                Mincharge = @Mincharge
                            WHERE RecordID = @RecordID
                              AND PartNo = @PartNo";

                    await SqlDataAccess.UpdateInsertQuery(updateDaily, new
                    {
                        mold.RecordID,
                        PartNo = part,
                        mold.CycleShot,
                        Total = newTotal,
                        mold.MachineNo,
                        mold.Remarks,
                        mold.Mincharge
                    });

                    // Update / Insert MONITOR
                    string monitorUpdate = @"
                        UPDATE DieMoldMonitor
                        SET TotalDie = @TotalDie
                        WHERE PartNo = @PartNo
                          AND CAST(DateAction AS DATE) = @DateAction;

                        IF @@ROWCOUNT = 0
                        BEGIN
                            INSERT INTO DieMoldMonitor (PartNo, DateAction, TotalDie)
                            VALUES (@PartNo, @DateAction, @TotalDie)
                        END";

                    await SqlDataAccess.UpdateInsertQuery(monitorUpdate, new
                    {
                        PartNo = part,
                        TotalDie = mold.CycleShot,
                        DateAction = mold.DateInput
                    });
                }

                Debug.WriteLine("✔ DieSerial update applied to ALL related PartNo");
                return true;
            }
        }
        public Task<bool> ChangeStatsDaily(int ID, int Stats)
        {
            string strsql = $@"UPDATE DieMold_Daily SET Status =@Status WHERE RecordID =@RecordID";
            return SqlDataAccess.UpdateInsertQuery(strsql, new { Status = Stats, RecordID = ID });
        }
        public async Task<bool> CheckMoldieExist(string searchValue, string Dateinput)
        {
            bool isPartNo = !string.IsNullOrWhiteSpace(searchValue)
                           && searchValue.StartsWith("0");

            string filter = isPartNo ? "PartNo =@Searchval" : "DieSerial =@Searchval";

            string sql = $@"SELECT Count
                       FROM DieMoldDailyInputChecker
                       WHERE {filter}
                      AND CAST(DateInput AS DATE) = @DateInput;";

            int count = await SqlDataAccess.GetCountData(sql, new { Searchval = searchValue, DateInput = Dateinput });

            return count > 2;

        }
        public Task<bool> DeleteDailyMoldie(int ID)
        {
            throw new NotImplementedException();
        }
        public Task<bool> AddUpdateMainMoldie(DieMoldMonitoringModel mold, int action)
        {
            throw new NotImplementedException();
        }

        // ===========================================================
        // MOLD DIE TOOLING FUNCTIONALITY
        // ===========================================================
        public async Task<PagedResult<DieMoldToolingModelDisplay>> GetMoldToolingData(
            string search,
            string filter,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;
            string filterCondition = filter != "" ? $@"AND CAST(t.DateArrived AS DATE) = {filter} " : "";

            string strquery = $@"SELECT t.RecordID, t.RegNo, t.PartNo, p.Dimension_Quality, 
                                    t.Item, t.DetailsModify, t.ShotRelease,
                                    t.DateArrived,
                                    t.DateRepair,
                                    t.Incharge, t.Remarks
                                FROM DieMoldDieTooling t
                                INNER JOIN DieMold_MoldingMainParts p ON t.PartNo = p.PartNo
                                WHERE t.IsDeleted = 0 AND  (
                                    @Search IS NULL
                                    OR t.RegNo LIKE '%' + @Search + '%'
                                    OR t.PartNo LIKE '%' + @Search + '%'
                                ) {filterCondition}     
                                ORDER BY t.RecordID DESC
                                OFFSET @Offset ROWS
                                FETCH NEXT @PageSize ROWS ONLY";

            var items = await SqlDataAccess.GetData<DieMoldToolingModelDisplay>(strquery,
                         new
                         {
                             Search = string.IsNullOrWhiteSpace(search) ? null : search,
                             Offset = offset,
                             PageSize = pageSize
                         });

            int TotalRecords = items.Count;



            return new PagedResult<DieMoldToolingModelDisplay>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = TotalRecords
            };

        }

        public async Task<bool> AddUpdateMoldieTooling(DieMoldToolingModelDisplay mold, int action)
        {
            if(action == 0)
            {
                string insertquery = @"INSERT INTO DieMoldDieTooling(RegNo, PartNo, Item, DetailsModify, ShotRelease, 
                                     DateArrived, DateRepair, Incharge, Remarks)
                                   VALUES(@RegNo, @PartNo, @Item, @DetailsModify, @ShotRelease, @DateArrived , @DateRepair, 
                                    @Incharge, @Remarks)";
               
                return await SqlDataAccess.UpdateInsertQuery(insertquery, mold, "MoldTooling");
            }
            else
            {
                string insertquery = @"UPDATE DieMoldDieTooling SET RegNo =@RegNo, Item =@Item, DetailsModify =@DetailsModify, ShotRelease =@ShotRelease, 
                                    DateArrived =@DateArrived, DateRepair =@DateRepair, Incharge =@Incharge, Remarks =@Remarks
                                   WHERE RecordID =@RecordID";
            
                return await SqlDataAccess.UpdateInsertQuery(insertquery, mold, "MoldTooling");
            }
        }
        public Task<bool> DeleteMoldieTooling(int ID)
        {
            return SqlDataAccess.UpdateInsertQuery("UPDATE DieMoldDieTooling SET IsDeleted = 1 WHERE RecordID =@RecordID", new { RecordID = ID });
        }

        public async Task UpsertDailyAsync(string partNo, string date, int cycleShot, int total, int status, DieMoldMonitoringModel mold)
        {
            bool checkCycle = cycleShot == 0;

            int TotalCycle = checkCycle ? 0 : total;
            int NewStats = checkCycle ? 2 : status;

           

            string sql = @"
                    INSERT INTO DieMold_Daily
                    (PartNo, DateInput, CycleShot, Total, MachineNo, Remarks, Mincharge, Status)
                    VALUES
                    (@PartNo, @DateInput, @CycleShot, @Total, @MachineNo, @Remarks, @Mincharge, @Status);";

            await SqlDataAccess.UpdateInsertQuery(sql, new
            {
                PartNo = partNo,
                DateInput = date,
                CycleShot = cycleShot,
                Total = TotalCycle,
                MachineNo = mold.MachineNo,
                Remarks = mold.Remarks,
                Mincharge = mold.Mincharge,
                Status = NewStats
            });
        }

        public async Task UpsertMonitor(string partNo, string date, int cycleShot)
        {
            string sql = @"
                UPDATE DieMoldMonitor
                SET TotalDie = @TotalDie
                WHERE PartNo = @PartNo
                  AND CAST(DateAction AS DATE) = CAST(@DateAction AS DATE);

                IF @@ROWCOUNT = 0
                BEGIN
                    INSERT INTO DieMoldMonitor (PartNo, DateAction, TotalDie)
                    VALUES (@PartNo, @DateAction, @TotalDie);
                END";

            await SqlDataAccess.UpdateInsertQuery(sql, new
            {
                PartNo = partNo,
                DateAction = date,
                TotalDie = cycleShot
            });
        }




        // ===========================================================
        // MOLD DIE MASTERLIST 
        // ===========================================================
        public Task<bool> CheckMoldieMasterlist(string partno)
        {
            return SqlDataAccess.Checkdata($@"SELECT COUNT(PartNo) FROM DieMold_MoldingMainParts 
                                        WHERE PartNo =@PartNo",
                           new { PartNo = partno });
        }

        public async Task<DieMoldMonitoringModel> GetMoldieMasterlistParts(string partno)
        {
            bool isPartNo = !string.IsNullOrWhiteSpace(partno)
                          && partno.StartsWith("0");


            string sql = isPartNo
                ? @"SELECT 
                      PartNo,PartDescription
                     ,Dimension_Quality,DieSerial
                     ,DieNumber
                FROM DieMold_MoldingMainParts
                WHERE PartNo = @searchValue"
                : @"SELECT 
                      PartNo,PartDescription
                     ,Dimension_Quality,DieSerial
                     ,DieNumber
                FROM DieMold_MoldingMainParts
                WHERE DieSerial = @searchValue";

            var items = await  SqlDataAccess.GetData<DieMoldMonitoringModel>(
                sql,
                new
                {
                    SearchValue = partno?.Trim()
                });

            return items.FirstOrDefault();  
        }

        public Task<List<DieMoldMonitoringModel>> GetMoldieDieSerialParts(string diepart)
        {
             string strsql = @" SELECT 
                        m.PartNo, m.PartDescription
                        ,m.Dimension_Quality, m.DieSerial
                        ,m.DieNumber,
		                (
                        SELECT TOP 1 d.Status
                        FROM DieMold_Daily d
                        WHERE d.PartNo = m.PartNo
                        ORDER BY d.DateInput DESC   
                    ) AS Status
                FROM DieMold_MoldingMainParts m
                WHERE m.DieSerial = @DieSerial";
            return SqlDataAccess.GetData<DieMoldMonitoringModel>(strsql, new { DieSerial = diepart });
        }



        public async Task<PagedResult<DieMoldMonitoringModel>> GetMoldieMasterlist(
            string search,
            string filter,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;
            string filterCondition = filter != "" ? $@"AND PartDescription = '{filter}' " : "";

            var items = await SqlDataAccess.GetData<DieMoldMonitoringModel>($@"
                                SELECT PartNo,PartDescription
                                      ,Dimension_Quality,DieSerial
                                      ,DieNumber
                                FROM DieMold_MoldingMainParts
                                WHERE (
                                    @Search IS NULL
                                    OR PartNo LIKE '%' + @Search + '%'
                                    OR DieSerial LIKE '%' + @Search + '%'
                                ) {filterCondition} 
                              ORDER BY PartNo
                              OFFSET @Offset ROWS
                              FETCH NEXT @PageSize ROWS ONLY",
                          new
                          {
                              Search = string.IsNullOrWhiteSpace(search) ? null : search,
                              Offset = offset,
                              PageSize = pageSize
                          });

            int TotalRecords = items.Count;

            return new PagedResult<DieMoldMonitoringModel>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = TotalRecords
            };
        }

        public Task<bool> AddUpdateMoldieMasterlist(DieMoldMonitoringModel mold, int action)
        {
            if(action == 0)
            {
                string insertquery = @"INSERT INTO DieMold_MoldingMainParts(PartNo, PartDescription, Dimension_Quality, DieSerial, DieNumber, ProcessID)
                                   VALUES(@PartNo, @PartDescription, @Dimension_Quality, @DieSerial, @DieNumber, @ProcessID)";
                return SqlDataAccess.UpdateInsertQuery(insertquery, mold, "MoldieMasterlist");
            }
            else
            {
                string insertquery = @"UPDATE DieMold_MoldingMainParts SET PartDescription =@PartDescription, Dimension_Quality =@Dimension_Quality,
                                    DieSerial =@DieSerial, DieNumber =@DieNumber
                                   WHERE PartNo =@PartNo";
                return SqlDataAccess.UpdateInsertQuery(insertquery, mold, "MoldieMasterlist");
            }
        }










        public Task<bool> UpdateDailyLastCycle(int recordID, int lastcycle)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMoldieMasterlist(string partno)
        {
            return SqlDataAccess.UpdateInsertQuery("DELETE FROM DieMold_MoldingMainParts WHERE PartNo =@PartNo", new { PartNo = partno });
        }

        public async Task<List<string>> GetSamePartsNo(string dieSerial, string processId, string currentPart)
        {
            return await SqlDataAccess.GetlistStrings(@"
                SELECT DISTINCT PartNo
                FROM DieMold_MoldingMainParts
                WHERE DieSerial = @DieSerial
                  AND ProcessID = @ProcessID
                  AND PartNo <> @PartNo",
                new
                {
                    DieSerial = dieSerial,
                    ProcessID = processId,
                    PartNo = currentPart
                });
        }

  
        public (int Total, int Status) CalculateTotalAndStatus(
          int lastTotal,
          int cycleShot,
          string processId,
          string dimensionQuality)
        {

            int newTotal = lastTotal + cycleShot;
            int status = 0;

            dimensionQuality = dimensionQuality ?? "";

            // Highest priority rules first
            if (dimensionQuality.Contains("R60-25") && processId == "M002")
            {
                if (newTotal >= 38000 && newTotal <= 39999) status = 1;
                else if (newTotal > 40000) { status = 2; newTotal = 0; }
                return (newTotal, status);
            }

            if (dimensionQuality.Contains("CRV40-56"))
            {
                if (newTotal >= 28000 && newTotal <= 29999) status = 1;
                else if (newTotal > 30000) { status = 2; newTotal = 0; }
                return (newTotal, status);
            }

            if (processId == "M003")
            {
                if (newTotal >= 68000 && newTotal <= 69999) status = 1;
                else if (newTotal > 70000) { status = 2; newTotal = 0; }
                return (newTotal, status);
            }

            if (processId == "M005")
            {
                if (newTotal >= 28000 && newTotal <= 29999) status = 1;
                else if (newTotal > 30000) { status = 2; newTotal = 0; }
                return (newTotal, status);
            }

            if (processId == "M002" || processId == "M003")
            {
                if (newTotal >= 48000 && newTotal <= 49999) status = 1;
                else if (newTotal > 50000) { status = 2; newTotal = 0; }
            }

            return (newTotal, status);
        }

        public Task<bool> ChangeStatsSerialDaily(string datestring, string dieSerial, int Stats)
        {


            string strsql = $@"UPDATE d
                            SET d.Status =@Status 
                            FROM DieMold_Daily d
                            INNER JOIN DieMold_MoldingMainParts p
                                ON d.PartNo = p.PartNo
                            WHERE p.DieSerial = @DieSerial AND CAST(d.DateInput AS DATE) = CAST(@DateInput  AS DATE);";
            return SqlDataAccess.UpdateInsertQuery(strsql, new { Status = Stats, DieSerial = dieSerial, DateInput = datestring });
        }

        public async Task<bool> AddUpdateDailySerialMoldie(DieMoldDieSerialInput mold)
        {

            string getlastCycle = $@"SELECT TOP 1 ISNULL(d.Total, 0)
                                     FROM DieMold_Daily d 
                                    INNER JOIN DieMold_MoldingMainParts p
                                    ON d.PartNo = p.PartNo
                                     WHERE p.DieSerial = @DieSerial
                                    AND DateInput < @DateInput
                                    ORDER BY DateInput DESC";

            int lastCycle = await SqlDataAccess.GetCountData(getlastCycle,
                           new
                           {
                               DieSerial = mold.DieSerial,
                               DateInput = mold.DateInput
           });
            Debug.WriteLine("Last Cycle : " + lastCycle);

            int newCycle = lastCycle + mold.CycleShot;
            Debug.WriteLine("INputed Cycle  : " + mold.CycleShot);
            Debug.WriteLine("New Cycle : " + newCycle);

            //// =========================
            //// 2️⃣ Update daily table
            //// =========================


            ////Debug.WriteLine("Date Now : " + dailyDateinput);
            string dailyUpdate = @"UPDATE d
                                    SET 
                                       d.CycleShot = @CycleShot,
                                       d.Total = @Total,
                                       d.MachineNo = @MachineNo,
                                       d.Remarks = @Remarks,
                                       d.Mincharge = @Mincharge
                                    FROM DieMold_Daily d
                                    INNER JOIN DieMold_MoldingMainParts p
                                        ON d.PartNo = p.PartNo
                                    WHERE p.DieSerial = @DieSerial AND CAST(d.DateInput AS DATE) = CAST(@DateInput  AS DATE);";

            ////Debug.WriteLine("Current Date : " + yesterdayDailyStr.ToString());

            var parameters = new
            {
                DieSerial = mold.DieSerial,
                Total = newCycle,
                DateInput = mold.DateInput,
                CycleShot = mold.CycleShot,
                MachineNo = mold.MachineNo,
                Remarks = mold.Remarks,
                Mincharge = mold.Mincharge
            };

            bool dailyUpdated = await SqlDataAccess.UpdateInsertQuery(dailyUpdate, parameters);

            //Debug.WriteLine("Check Daily Update : " + dailyUpdated);
            if (!dailyUpdated) return false;

            //// =========================
            //// 3️⃣ Update / Insert monitor table
            //// =========================
            string monitorUpdate = @"
               UPDATE d
                SET d.TotalDie = @TotalDie
                FROM DieMoldMonitor d
                INNER JOIN DieMold_MoldingMainParts p
                    ON d.PartNo = p.PartNo
                WHERE p.DieSerial = @DieSerial
                  AND CAST(d.DateAction AS DATE) = @DateAction";

            return await SqlDataAccess.UpdateInsertQuery(monitorUpdate, new
            {
                TotalDie = mold.CycleShot,
                DateAction = mold.DateInput,
                DieSerial = mold.DieSerial
            });

        }




        public async void UpdatesAlltheTotalCycle(string partnum = "", string Die = "")
        {
            int TempValue = 0;
            List<DieMoldMonitoringModel> data = new List<DieMoldMonitoringModel>();
            // 1. Get the Data first
            if(partnum != "")
            {
                data = await GetPreviousMoldDieData(partnum);
            }

            if(Die != "")
            {
                data = await GetPreviousMoldieDataSerial(partnum);
            }

            for (int i = 0; i < data.Count(); i++)
            {
                // 2. Store the first value of data to the TempValue and then Skip to the next loop
                if (i == 0)
                {
                    TempValue = data[0].CycleShot;
                    // proceed to the next loop
                    continue;
                }

                // 3. Add the previous to the next value record
                TempValue += data[i].CycleShot;

                //  4. Updates to the Query  Record by ID
                // Updates the 
                string updatemold = $@"UPDATE DieMold_Daily SET CycleShot =@CycleShot, Total =@Total
                           WHERE RecordID =@RecordID";

                await SqlDataAccess.UpdateInsertQuery(updatemold, new
                {
                    CycleShot = data[i].CycleShot,
                    Total = TempValue,
                    RecordID = data[i].RecordID
                });

            }
        }







        // =================== FOR THE UPDATE PROCESS ============================
        public Task<List<DieMoldMonitoringModel>> GetPreviousMoldDieData(string partnum)
        {
            string strsql = @"WITH OrderedData AS (
                            SELECT *,
                                   ROW_NUMBER() OVER (ORDER BY DateInput DESC, RecordID DESC) AS rn
                            FROM PMACS_LIVE.dbo.DieMold_Daily
	                        WHERE PartNo = @PartNo
                        ),
                        StopPoint AS (
                            SELECT MIN(rn) AS stop_rn
                            FROM OrderedData
                            WHERE Total = 0
                        )
                        SELECT o.*
                        FROM OrderedData o
                        CROSS JOIN StopPoint s
                        WHERE o.rn < s.stop_rn
                        ORDER BY o.DateInput ASC, o.RecordID ASC;";

            return SqlDataAccess.GetData<DieMoldMonitoringModel>(strsql, new { PartNo = partnum });

        }


        public Task<List<DieMoldMonitoringModel>> GetPreviousMoldieDataSerial(string DieSerial)
        {
            string strsql = @"WITH OrderedData AS (
                                SELECT d.*,
                                       ROW_NUMBER() OVER (
                                           ORDER BY d.DateInput DESC, d.RecordID DESC
                                       ) AS rn
                                FROM PMACS_LIVE.dbo.DieMold_Daily d
                                INNER JOIN PMACS_LIVE.dbo.DieMold_MoldingMainParts m
                                    ON m.PartNo = d.PartNo
                                WHERE m.DieSerial = @DieSerial
                            ),
                            StopPoint AS (
                                SELECT MIN(rn) AS stop_rn
                                FROM OrderedData
                                WHERE Total = 0
                            )
                            SELECT o.*
                            FROM OrderedData o
                            CROSS JOIN StopPoint s
                            ORDER BY o.DateInput DESC, o.RecordID DESC;"
            ;

            return SqlDataAccess.GetData<DieMoldMonitoringModel>(strsql, new { DieSerial = DieSerial });
        }


        public Task<List<string>> GetPartNoByDieSerial(string dieSerial)
        {
            string sql = @"
                SELECT PartNo
                FROM DieMold_MoldingMainParts
                WHERE DieSerial = @DieSerial";

            return SqlDataAccess.GetData<string>(sql, new { DieSerial = dieSerial });
        }
    }
}