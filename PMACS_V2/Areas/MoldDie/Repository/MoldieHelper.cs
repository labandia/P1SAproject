using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PMACS_V2.Areas.MoldDie.Repository
{
    public static class MoldieHelper
    {
        // ===========================================================
        // MOLD DIE PART NUMBER LOGIC
        // ===========================================================
        public static async Task<bool> MoldiePartNoLogic(
            DieMoldMonitoringModel mold, string dateOnly)
        {
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

            var history = await GetMasterlistParts(mold.PartNo);

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

            await UpdateInputChecker("PartNo", mold.PartNo, dateOnly);

            Debug.WriteLine("Done Proess Completed !!!!!!!!! ... ");
            return true;

        }


        // GET MOLDIE PARTS MASTERLIST 

        public static async Task<DieMoldMonitoringModel> GetMasterlistParts(string partno)
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

            var items = await SqlDataAccess.GetData<DieMoldMonitoringModel>(
                sql, new { SearchValue = partno?.Trim() });

            return items.FirstOrDefault();
        }


        public static (int Total, int Status) CalculateTotalAndStatus(
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


        public static async Task UpsertDailyAsync(string partNo,
           string date,
           int cycleShot,
           int total,
           int status, DieMoldMonitoringModel mold)
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

        public static async Task UpsertMonitor(string partNo, string date, int cycleShot)
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




        public static async Task<List<string>> GetSamePartsNo(string dieSerial, string processId, string currentPart)
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

        // ===========================================================
        // MOLD DIE SERIAL NUMBER LOGIC
        // ===========================================================

        public static async Task<bool> MoldieDieSerialLogic(DieMoldMonitoringModel mold, string dateOnly)
        {
            // Get all related PartNo using DieSerial
            var partList = await GetPartNoByDieSerial(mold.DieSerial);
            if (!partList.Any()) return false;


            foreach (var part in partList)
            {
                // Get last total BEFORE this date
                string getLastTotal = @"  
                            SELECT TOP 1 ISNULL(Total, 0)
                            FROM DieMold_Daily d
                            INNER JOIN DieMold_MoldingMainParts p 
                            ON d.PartNo = p.PartNo
                            WHERE p.DieSerial = @DieSerial
                              AND d.DateInput < @DateInput
                            ORDER BY DateInput DESC, RecordID DESC";

                int lastTotal = await SqlDataAccess.GetCountData(getLastTotal, new
                {
                    DieSerial = mold.DieSerial,
                    DateInput = mold.DateInput
                });

                Debug.WriteLine("Last Cycle Time Die Serial : " + lastTotal);


                var histories = await GetByDieSerialAsync(mold.DieSerial);

                var history = histories
                        .OrderByDescending(x => x.PartNo)   // or DieNumber / CreatedDate
                        .FirstOrDefault();

                //if (history == null) return false;
                Debug.WriteLine("Done Getting the Last Record History  ... ");

                if (history == null) return false;

                var dimensionQuality = history.Dimension_Quality;

                if (!histories.Any()) return false;

                // correct Dimension_Quality per PartNo
                var (moldtotal, moldstatus) = CalculateTotalAndStatus(
                  lastTotal,
                  mold.CycleShot,
                  mold.ProcessID,
                  dimensionQuality);

                Debug.WriteLine("Done Calculating Total and Status ... ");

                // =====================================================
                // DAILY MONITORING 
                // =====================================================
                await UpsertDailySerialAsync(part, mold.DateInput, mold.CycleShot, moldtotal, moldstatus, mold);
                Debug.WriteLine("Done Updating Daily ... ");

                // =====================================================
                // INSERT MONITOR LOG
                // =====================================================

                await UpsertMonitor(part, dateOnly, mold.CycleShot);

                Debug.WriteLine("Done Updating Monitoring ... ");


            }

            await UpdateInputChecker("DieSerial", mold.DieSerial, dateOnly);

            Debug.WriteLine("✔ DieSerial update applied to ALL related PartNo");
            return true;

        }



        public static Task<List<string>> GetPartNoByDieSerial(string dieSerial)
        {
            string sql = @"
                SELECT PartNo
                FROM DieMold_MoldingMainParts
                WHERE DieSerial = @DieSerial";

            return SqlDataAccess.GetData<string>(sql, new { DieSerial = dieSerial });
        }

        public static async Task<List<DieMoldMonitoringModel>> GetByDieSerialAsync(string dieSerial)
        {
            const string sql = @"
                SELECT PartNo, PartDescription,
                       Dimension_Quality, DieSerial, DieNumber
                FROM DieMold_MoldingMainParts
                WHERE DieSerial = @DieSerial";

            return (await SqlDataAccess.GetData<DieMoldMonitoringModel>(
                sql, new { DieSerial = dieSerial.Trim() })).ToList();
        }


        public static async Task UpsertDailySerialAsync(string partNo,
          string date,
          int cycleShot,
          int total,
          int status, DieMoldMonitoringModel mold)
        {
            bool checkCycle = cycleShot == 0;

            int TotalCycle = checkCycle ? 0 : total;
            int NewStats = checkCycle ? 2 : status;

            string sql = @"
                IF NOT EXISTS (
                    SELECT 1
                    FROM DieMold_Daily
                    WHERE PartNo = @PartNo
                      AND DateInput = @DateInput
                )
                BEGIN
                    INSERT INTO DieMold_Daily
                    (PartNo, DateInput, CycleShot, Total, MachineNo, Remarks, Mincharge, Status)
                    VALUES
                    (@PartNo, @DateInput, @CycleShot, @Total, @MachineNo, @Remarks, @Mincharge, @Status)
                END";

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


        // ===========================================================
        // COMMON FUNCTION 
        // ===========================================================

        private static async Task UpdateInputChecker(
            string column, 
            string value, 
            string dateinput)
        {
            string sql = $@"
                    IF EXISTS (
                        SELECT 1 FROM DieMoldDailyInputChecker
                        WHERE {column} = @Value AND DateInput = @DateInput
                    )
                    BEGIN
                        UPDATE DieMoldDailyInputChecker
                        SET Count = ISNULL(Count, 0) + 1
                        WHERE {column} = @Value AND DateInput = @DateInput
                    END
                    ELSE
                    BEGIN
                        INSERT INTO DieMoldDailyInputChecker ({column}, DateInput, Count)
                        VALUES (@Value, @DateInput, 1)
                    END";
            await SqlDataAccess.UpdateInsertQuery(sql,
                        new { Value = value, DateInput = dateinput });
        }
    }
}