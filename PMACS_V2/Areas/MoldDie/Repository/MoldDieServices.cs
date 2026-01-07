using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Web.UI.WebControls.WebParts;
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
            Debug.WriteLine($@"Month : {month} - Days : {days} - Year : {year} - Process : {process}");
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
        public Task<List<DieMoldMonitoringModel>> GetDailyMoldHistoryData(string partnum, string processID)
        {
            return SqlDataAccess.GetData<DieMoldMonitoringModel>($@"SELECT 
                            d.RecordID,
	                        FORMAT(d.DateInput, 'MM/dd/yy') as DateInput, 
	                        d.PartNo, p.Dimension_Quality, 
                            p.DieSerial,
	                        d.CycleShot, 
	                        d.Total, 
	                        d.MachineNo, 
	                        d.Status, 
	                        d.Remarks, 
	                        d.Mincharge
                        FROM DieMold_Daily d 
                        INNER JOIN DieMold_MoldingMainParts p ON d.PartNo = p.PartNo
                        WHERE 
                            d.PartNo = @PartNo AND p.ProcessID = @ProcessID
                        ORDER BY d.RecordID DESC", new { PartNo = partnum, ProcessID = processID });
        }
        

        public async Task<bool> AddUpdateDailyMoldie(DieMoldMonitoringModel mold, int action)
        {
             if (action == 0)
            {
                // Check if part exists
                string checkquery = "SELECT COUNT(PartNo) FROM DieMold_Daily WHERE PartNo = @PartNo";
                bool IsExist = await SqlDataAccess.Checkdata(checkquery, new { PartNo = mold.PartNo });


                // ===========================================================
                // CASE 1: Part number already exists → compute new total
                // ===========================================================
                if (IsExist)
                {
                    var dateOnly = mold.DateInput.Date;
                    // Get last total shot
                    int lasttotal = await SqlDataAccess.GetCountData($@"
                    SELECT TOP 1 Total
                    FROM DieMold_Daily
                    WHERE PartNo = @PartNo 
                    ORDER BY RecordID DESC",
                        new { PartNo = mold.PartNo });


                    // ===== Step 2: Compute new total =====
                    int newtotalshot = lasttotal + mold.CycleShot;
                    int UpdateStatus = 0; // 0 = Continue

                    var partnuminfo = (await GetDailyMoldHistoryData(
                        mold.PartNo, mold.ProcessID))
                        .FirstOrDefault();

                    if (partnuminfo == null) return false;


                    string dq = partnuminfo.Dimension_Quality ?? "";
                    string pid = mold.ProcessID;
                    string serial = mold.DieSerial;


                    // ===========================
                    // RULE: M002 & M003 (General)
                    // ===========================
                    if (pid == "M002" || pid == "M003")
                    {
                        if (newtotalshot >= 48000 && newtotalshot <= 49999)
                        {
                            UpdateStatus = 1; // Cleaning
                        }
                        else if (newtotalshot > 50000)
                        {
                            UpdateStatus = 2; // Needed
                            newtotalshot = 0;
                        }
                    }

                    // ===================================
                    // RULE: R60-25 (contains) + M002 only
                    // ===================================
                    if (dq.Contains("R60-25") && pid == "M002")
                    {
                        if (newtotalshot >= 38000 && newtotalshot <= 39999)
                        {
                            UpdateStatus = 1;
                        }
                        else if (newtotalshot > 40000)
                        {
                            UpdateStatus = 2;
                            newtotalshot = 0;
                        }
                    }


                    // ===========================
                    // RULE: M003 (Specific Option)
                    // ===========================
                    if (pid == "M003")
                    {
                        if (newtotalshot >= 68000 && newtotalshot <= 69999)
                        {
                            UpdateStatus = 1;
                        }
                        else if (newtotalshot > 70000)
                        {
                            UpdateStatus = 2;
                            newtotalshot = 0;
                        }
                    }


                    // ===========================
                    // RULE: M005
                    // ===========================
                    if (pid == "M005")
                    {
                        if (newtotalshot >= 28000 && newtotalshot <= 29999)
                        {
                            UpdateStatus = 1;
                        }
                        else if (newtotalshot > 30000)
                        {
                            UpdateStatus = 2;
                            newtotalshot = 0;
                        }
                    }

                    // =============================
                    // RULE: CRV40-56 (contains ANY)
                    // =============================
                    if (dq.Contains("CRV40-56"))
                    {
                        if (newtotalshot >= 28000 && newtotalshot <= 29999)
                        {
                            UpdateStatus = 1;
                        }
                        else if (newtotalshot > 30000)
                        {
                            UpdateStatus = 2;
                            newtotalshot = 0;
                        }
                    }


                    // ===== Step 4: Insert data =====
                    string insertquery = @"
                            INSERT INTO DieMold_Daily
                            (PartNo, DateInput, CycleShot, Total, MachineNo, Remarks, Mincharge, Status)
                            VALUES (@PartNo, @DateInput, @CycleShot, @Total, @MachineNo, @Remarks, @Mincharge, @Status)";

                    var parameters = new
                    {
                        PartNo = mold.PartNo,
                        DateInput = dateOnly,
                        CycleShot = mold.CycleShot,
                        Total = newtotalshot,
                        MachineNo = mold.MachineNo,
                        Remarks = mold.Remarks,
                        Mincharge = mold.Mincharge,
                        Status = UpdateStatus
                    };
                    
                    await SqlDataAccess.UpdateInsertQuery(insertquery, parameters);

                    // =====================================================
                    // INSERT MONITOR LOG
                    // =====================================================

                    string strmonitor = $@"
                        INSERT INTO DieMoldMonitor(PartNo, DateAction, TotalDie)
                        VALUES(@PartNo, @DateAction, @TotalDie)";

                    await SqlDataAccess.UpdateInsertQuery(strmonitor, new
                    {
                        PartNo = mold.PartNo,
                        DateAction = dateOnly,
                        TotalDie = mold.CycleShot
                    });
                    // =====================================================
                    // 🔥 UPDATE OTHER PART NUMBERS WITH SAME DIE SERIAL
                    // =====================================================

                    var relatedParts = await SqlDataAccess.GetlistStrings(@"
                            SELECT DISTINCT PartNo
                            FROM DieMold_MoldingMainParts
                            WHERE (DieSerial = @DieSerial AND ProcessID = @ProcessID) AND PartNo <> @PartNo",
                            new {
                                DieSerial = mold.DieSerial,
                                ProcessID = pid,
                                PartNo = mold.PartNo 
                            });
                    Debug.WriteLine("HERE");
                    foreach (var part in relatedParts) {
                        Debug.WriteLine(part);
                        await SqlDataAccess.UpdateInsertQuery(insertquery, 
                            new { 
                                PartNo = part, 
                                DateInput = dateOnly, 
                                CycleShot = newtotalshot, 
                                Total = newtotalshot, 
                                mold.MachineNo, 
                                Remarks = "Auto update (same DieSerial)", 
                                mold.Mincharge, 
                                Status = UpdateStatus
                            }); 
                    }
                



                    return true;
                }
                else
                {
                    // =====================================================
                    // NEW PART NUMBER
                    // =====================================================
                    var dateOnly = mold.DateInput.Date;

                    string insertquery = @"
                        INSERT INTO DieMold_Daily
                        (PartNo, DateInput, CycleShot, Total, MachineNo, Remarks, Mincharge, Status)
                        VALUES (@PartNo, @DateInput, @CycleShot, @Total, @MachineNo, @Remarks, @Mincharge, @Status)";

                    var parameters = new
                    {
                        PartNo = mold.PartNo,
                        DateInput = mold.DateInput,
                        CycleShot = mold.CycleShot,
                        Total = mold.CycleShot,
                        MachineNo = mold.MachineNo,
                        Remarks = mold.Remarks,
                        Mincharge = mold.Mincharge,
                        Status = 0
                    };

                    string strmonitor = $@"
                        INSERT INTO DieMoldMonitor(PartNo, DateAction, TotalDie)
                        VALUES(@PartNo, @DateAction, @TotalDie)";

                    await SqlDataAccess.UpdateInsertQuery(strmonitor, new
                    {
                        PartNo = mold.PartNo,
                        DateAction = mold.DateInput,
                        TotalDie = mold.CycleShot
                    });

                    await SqlDataAccess.UpdateInsertQuery(insertquery, parameters);
                    // =====================================================
                    // 🔥 UPDATE OTHER PART NUMBERS WITH SAME DIE SERIAL
                    // =====================================================
                    Debug.WriteLine("HERE 2");
                    var relatedParts = await SqlDataAccess.GetlistStrings(@"
                            SELECT DISTINCT PartNo
                            FROM DieMold_MoldingMainParts
                            WHERE (DieSerial = @DieSerial AND ProcessID = @ProcessID) AND PartNo <> @PartNo",                       
                            new
                            {
                                DieSerial = mold.DieSerial,
                                ProcessID = mold.ProcessID,
                                PartNo = mold.PartNo
                            });
                    foreach (var part in relatedParts)
                    {
                        await SqlDataAccess.UpdateInsertQuery(insertquery,
                            new
                            {
                                PartNo = part,
                                DateInput = dateOnly,
                                CycleShot = mold.CycleShot,
                                Total = mold.CycleShot,
                                mold.MachineNo,
                                Remarks = "Auto update (same DieSerial)",
                                mold.Mincharge,
                                Status = 0
                            });
                    }

                    return true;
                }
            }
            else
            {
                string insertquery = @"
                UPDATE DieMold_Daily SET DateInput = @DateInput, PartNo =@PartNo, CycleShot = @CycleShot,  
                MachineNo =@MachineNo, Remarks = @Remarks, Mincharge = @Mincharge
                WHERE RecordID =@RecordID";

                var parameters = new
                {
                    RecordID = mold.RecordID,
                    DateInput = mold.DateInput,
                    PartNo = mold.PartNo,
                    CycleShot = mold.CycleShot,
                    MachineNo = mold.MachineNo,
                    Remarks = mold.Remarks,
                    Mincharge = mold.Mincharge
                };

                string updatequery = $@"UPDATE DieMoldMonitor SET TotalDie =@TotalDie 
                                    WHERE CAST(DateAction AS DATE) = @DateAction AND PartNo = @PartNo";
                var parameter = new { TotalDie = mold.CycleShot, DateAction = mold.DateInput, PartNo = mold.PartNo };

                bool result = await SqlDataAccess.UpdateInsertQuery(updatequery, parameter);


                return result ? await SqlDataAccess.UpdateInsertQuery(insertquery, parameters) : false;
            }
        }
        public Task<bool> ChangeStatsDaily(int ID, int Stats)
        {
            string strsql = $@"UPDATE DieMold_Daily SET Status =@Status WHERE RecordID =@RecordID";
            return SqlDataAccess.UpdateInsertQuery(strsql, new { Status = Stats, RecordID = ID });
        }
        public Task<bool> CheckMoldieExist(string partnum, string Dateinput)
        {
            return SqlDataAccess.Checkdata($@"SELECT COUNT(PartNo) FROM DieMold_Daily 
                                        WHERE PartNo =@PartNo AND CAST(DateInput AS DATE) = @DateInput",
                                       new { PartNo = partnum, DateInput = Dateinput });
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

        // ===========================================================
        // MOLD DIE MASTERLIST 
        // ===========================================================
        public Task<bool> CheckMoldieMasterlist(string partno)
        {
            return SqlDataAccess.Checkdata($@"SELECT COUNT(PartNo) FROM DieMold_MoldingMainParts 
                                        WHERE PartNo =@PartNo",
                           new { PartNo = partno });
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

       
    }
}